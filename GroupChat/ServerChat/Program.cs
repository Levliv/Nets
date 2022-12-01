namespace ServerChat;

class Program
{
  static async Task Main(string[] args)
  {
    if (!int.TryParse(args[0], out int port))
    {
      Console.WriteLine("Please, specify the port");
    }
    Server server = new (port);
    await server.StartUpListenAsync(); 
  } 
}