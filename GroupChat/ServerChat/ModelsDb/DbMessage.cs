namespace ServerChat.ModelsDb;

public class DbMessage
{
  public Guid Id { get; set; }
  public Guid ChatId { get; set; }
  public Guid SenderId { get; set; }
  public string? Content { get; set; }
  public DateTime SendDateTimeAtUtc { get; set; }
}
