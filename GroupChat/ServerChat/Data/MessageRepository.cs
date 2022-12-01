using ServerChat.ModelsDb;

namespace ServerChat.Data;

public class MessageRepository
{
  public async Task<Guid> CreateAsync(string login, string content)
  {
    Guid messageId = Guid.NewGuid();
    await using ApplicationContext db = new ();
    DbMessage dbMessage = new()
    {
      Id = messageId,
      Login = login,
      Content = content,
      SendDateTimeAtUtc = DateTime.UtcNow
    };
    db.Messages.Add(dbMessage);
    await db.SaveChangesAsync();

    return messageId;
  }
}
