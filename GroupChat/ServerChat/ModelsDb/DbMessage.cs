namespace ServerChat.ModelsDb;

public class DbMessage
{
  public Guid Id { get; set; }
  public string Login { get; set; }
  public string? Content { get; set; }
  public DateTime SendDateTimeAtUtc { get; set; }
}
