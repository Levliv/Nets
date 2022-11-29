namespace ServerChat.ModelsDb;

public class DbGroup
{
  public Guid ChatId { get; set; }
  public string Name { get; set; } = null!;
  public string SecretCode { get; set; } = null!;
}
