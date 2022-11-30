using ServerChat.ModelsDb;

namespace ServerChat;

using Microsoft.EntityFrameworkCore;
 
public class ApplicationContext : DbContext
{
  public DbSet<DbUser> Users => Set<DbUser>();
  public DbSet<DbMessage> Messages => Set<DbMessage>();
  public DbSet<DbGroup> Groups => Set<DbGroup>();

  public ApplicationContext() => Database.EnsureCreated();
 
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=GroupChat;User Id=sa;Password=Testing1122;");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<DbUser>()
      .HasMany(dbUser => dbUser.Groups)
      .WithMany(dbGroup => dbGroup.Users)
      .UsingEntity(b => b.ToTable("GroupsUsers"));
  }
}