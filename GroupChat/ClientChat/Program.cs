using System.Net;

namespace ClientChat;

class Program
{
  static async Task Main(string[] args)
  {
    Client client = new ();
    
    if (!IPAddress.TryParse(args[0], out IPAddress? ipAddress))
    {
      Console.WriteLine("Entered ip is incorrect");
    }
    
    if (!int.TryParse(args[1], out int port))
    {
      Console.WriteLine("Entered port is incorrect");
    }
    
    string host = ipAddress!.ToString();
    await client.StartUp(host, port);
  }
}