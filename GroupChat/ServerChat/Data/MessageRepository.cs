using ServerChat.ModelsDb;

namespace ServerChat.Data;

public class MessageRepository
{
  public async Task<Guid> Create(Guid groupId, Guid senderId, string content)
  {
    Guid messageId = Guid.NewGuid();
    using (ApplicationContext db = new ApplicationContext())
    {
      DbMessage dbMessage = new()
      {
        Id = messageId,
        GroupId = groupId,
        SenderId = senderId,
        Content = content,
        SendDateTimeAtUtc = DateTime.UtcNow
      };
      db.Messages.Add(dbMessage);
      await db.SaveChangesAsync();
    }

    return messageId;
  }
}
