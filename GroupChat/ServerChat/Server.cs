using System.Net;
using System.Net.Sockets;

namespace ServerChat;

public class Server
{
  private readonly TcpListener _tcpListener;
  private readonly List<Client> _clients = new ();

  public Server(int port)
  {
    _tcpListener = new (IPAddress.IPv6Loopback, port);
  }
  
  public async Task StartUpListenAsync()
  {
    try
    {
      _tcpListener.Start();
      Console.WriteLine("Server is running...");
 
      while (true)
      {
        TcpClient tcpClient = await _tcpListener.AcceptTcpClientAsync();
        Client client = new (tcpClient, this);
        _clients.Add(client);
        Task.Run(client.ProcessAsync);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
    finally
    {
      Dispose();
    }
  }
 
  protected internal async Task SendBroadcastMessageAsync(string message, Guid id)
  {
    foreach (Client client in _clients.Where(client => client.Id != id))
    {
      await client.Writer.WriteLineAsync(message);
      await client.Writer.FlushAsync();
    }
  }
  
  protected internal void RemoveConnection(Guid id)
  {
    Client? client = _clients.FirstOrDefault(c => c.Id == id);
    if (client is not null)
    {
      _clients.Remove(client);
    }
    client?.Dispose();
  }
  
  private void Dispose()
  {
    foreach (Client client in _clients)
    {
      client.Dispose();
    }
    _tcpListener.Stop();
  }
}
