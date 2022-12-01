using ServerChat.ModelsDb;

namespace ServerChat.Business;

using ServerChat.Data;

public class Controller
{
  public async Task SaveMessageAsync(string senderLogin, string content)
  {
    MessageRepository messageRepository = new();
    await messageRepository.CreateAsync(senderLogin, content);
  }
}