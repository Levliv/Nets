using System.Net.Sockets;

namespace ClientChat;

public class Client
{
  private string? Login { get; set; }
  public async Task StartUp(string host, int port)
  {
    using TcpClient client = new ();
    Console.Write("Enter your login: ");
    Login = Console.ReadLine();
    StreamReader? streamReader = null;
    StreamWriter? streamWriter = null;
 
    try
    {
      client.Connect(host, port);
      streamReader = new StreamReader(client.GetStream());
      streamWriter = new StreamWriter(client.GetStream());
      Task.Run(()=>GetAsync(streamReader));
      await SendAsync(streamWriter);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
    streamWriter?.Close();
    streamReader?.Close();
  }
  async Task SendAsync(StreamWriter writer)
  {
    await writer.WriteLineAsync(Login);
    await writer.FlushAsync();
    Console.WriteLine("To send message, press \\Enter");
 
    while (true)
    {
      string? message = Console.ReadLine();
      await writer.WriteLineAsync(message);
      await writer.FlushAsync();
    }
  }

  async Task GetAsync(StreamReader reader)
  {
    while (true)
    {
      try
      {
        string? message = await reader.ReadLineAsync();
            
        if (string.IsNullOrEmpty(message))
        {
          continue;
        } 
        Console.WriteLine(message);
      }
      catch
      {
        break;
      }
    }
  }
}