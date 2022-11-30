using Microsoft.EntityFrameworkCore;
using ServerChat.ModelsDb;

namespace ServerChat.Data;

public class GroupRepository
{
  public async Task<List<DbMessage>?> GetMessages(Guid groupId)
  {
    await using ApplicationContext db = new ();
    return (await db.Groups.FindAsync(groupId))?.Messages.ToList();
  }

  public async Task<Guid?> Create(string name, string secretCode)
  {
    await using ApplicationContext db = new ();
    if (await db.Groups.Where(dbGroup => dbGroup.Name == name).FirstOrDefaultAsync() == null)
    {
      return null;
    }
    Guid groupId = Guid.NewGuid();
    DbGroup dbGroup = new()
    {
      Id = groupId,
      Name = name,
      SecretCode = secretCode
    };
    await db.SaveChangesAsync();
    
    return groupId;
  }
}