namespace ServerChat.ModelsDb;

public class DbUser
{
  public Guid Id { get; set; }
  public string? Login { get; set; }
  public string? Password { get; set; }
  public bool IsActive { get; set; }
  public DateTime RegDateTimeAtUtc { get; set; }
  public DateTime? LastActiveDateTimeAtUtc { get; set; }
}
