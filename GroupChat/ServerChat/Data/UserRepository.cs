using Microsoft.EntityFrameworkCore;
using ServerChat.ModelsDb;

namespace ServerChat.Data;

public class UserRepository
{

  public async Task<List<DbGroup>?> GetUserGroups(Guid userId)
  {
    await using ApplicationContext db = new ();
    List<DbGroup>? groups = (await db.Users.FindAsync(userId))?
      .Groups
      .ToList();
    
    return groups;
  }
    
  public async Task<Guid?> Create(string login, string password)
  {
    Guid userId = Guid.NewGuid();
    await using ApplicationContext db = new ();
    
    if (await db.Users.FirstOrDefaultAsync(user => user.Login == login) == null)
    {
      return null;
    }
    
    DbUser dbUser = new ()
    { 
      Id = userId,
      Login = login,
      Password = password,
      IsActive = true,
      RegDateTimeAtUtc = DateTime.UtcNow
    };
    db.Users.Add(dbUser);
    await db.SaveChangesAsync();
    
    return userId;
  }
}
