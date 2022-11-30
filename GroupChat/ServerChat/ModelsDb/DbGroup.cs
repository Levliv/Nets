namespace ServerChat.ModelsDb;

public class DbGroup
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public string SecretCode { get; set; } = null!;

  public List<DbUser> Users { get; set; } = new ();
  public List<DbMessage> Messages { get; set; } = new();
}
