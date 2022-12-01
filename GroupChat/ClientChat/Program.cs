using System.Net.Sockets;
 
const string host = "127.0.0.1";
const int port = 8000;
using TcpClient client = new TcpClient();
Console.Write("Введите свое имя: ");
string? userName = Console.ReadLine();
Console.WriteLine($"Добро пожаловать, {userName}");
StreamReader? streamReader = null;
StreamWriter? streamWriter = null;
 
try
{
    client.Connect(host, port);
    streamReader = new StreamReader(client.GetStream());
    streamWriter = new StreamWriter(client.GetStream());
    Task.Run(()=>ReceiveMessageAsync(streamReader));
    await SendMessageAsync(streamWriter);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
streamWriter?.Close();
streamReader?.Close();

async Task SendMessageAsync(StreamWriter writer)
{
    await writer.WriteLineAsync(userName);
    await writer.FlushAsync();
    Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");
 
    while (true)
    {
        string? message = Console.ReadLine();
        await writer.WriteLineAsync(message);
        await writer.FlushAsync();
    }
}

async Task ReceiveMessageAsync(StreamReader reader)
{
    while (true)
    {
        try
        {
            string? message = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(message)) continue; 
            Print(message);
        }
        catch
        {
            break;
        }
    }
}

void Print(string message)
{
    Console.WriteLine(message);
}