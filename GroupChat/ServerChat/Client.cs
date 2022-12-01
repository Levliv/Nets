using System.Net.Sockets;
using ServerChat.Business;

namespace ServerChat;

public class Client
{
  private StreamReader Reader { get;}
  private readonly TcpClient _client;
  private readonly Server _server;
  private readonly Controller _controller;
  
  public StreamWriter Writer { get;}
  
  public Guid Id { get; }
  
  public Client(TcpClient tcpClient, Server server)
  {
    _client = tcpClient;
    _server = server;
    _controller = new Controller();
    NetworkStream stream = _client.GetStream();
    Reader = new StreamReader(stream);
    Writer = new StreamWriter(stream);
    Id = Guid.NewGuid();
  }

  public async Task ProcessAsync()
  {
    try
    {
      string? login = await Reader.ReadLineAsync();
      string? message = $"INFO: {login} online";
      await _server.SendBroadcastMessageAsync(message, Id);
      Console.WriteLine(message);
      while (true)
      {
        message = await Reader.ReadLineAsync();

        if (message == null)
        {
          Console.WriteLine($"INFO: {login} disconnected");
          return;
        }

        await _controller.SaveMessageAsync(login, message);
        message = $"{login}: {message}";
        Console.WriteLine(message);
        await _server.SendBroadcastMessageAsync(message, Id);
      }
    }
    catch (Exception exception)
    {
      Console.WriteLine(exception.Message);
    }
    finally
    {
      _server.RemoveConnection(Id);
    }
  }

  protected internal void Dispose()
  {
    Writer.Close();
    Reader.Close();
    _client.Close();
  }
}
