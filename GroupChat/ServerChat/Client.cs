using System.Net.Sockets;
using ServerChat.Business;

namespace ServerChat;

public class Client
{
  private StreamReader Reader { get;}
  private readonly TcpClient _client;
  private readonly Server _server;
  private Controller _controller;
  
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
      string? userName = await Reader.ReadLineAsync();
      string? message = $"{userName} online";
      await _server.SendBroadcastMessageAsync(message, Id);
      Console.WriteLine(message);
      while (true)
      {
        try
        {
          message = await Reader.ReadLineAsync();
          
          if (message == null)
          {
            Console.WriteLine($"{userName}: disconnected");
            return;
          }

          await _controller.SaveMessageAsync(userName, message);
          message = $"{userName}: {message}";
          Console.WriteLine(message);
          await _server.SendBroadcastMessageAsync(message, Id);
        }
        catch
        {
          message = $"{userName} offline";
          Console.WriteLine(message);
          await _server.SendBroadcastMessageAsync(message, Id);
          break;
        }
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
