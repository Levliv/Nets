using ServerChat.ModelsDb;

namespace ServerChat;

using Microsoft.EntityFrameworkCore;
 
public class ApplicationContext : DbContext
{
  public DbSet<DbMessage> Messages => Set<DbMessage>();

  public ApplicationContext() => Database.EnsureCreated();
 
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=GroupChat;User Id=sa;Password=Testing1122;");
  }
}