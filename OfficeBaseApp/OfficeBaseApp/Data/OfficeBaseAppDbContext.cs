namespace OfficeBaseApp.Data;
using Microsoft.EntityFrameworkCore;
public class OfficeBaseAppDbContext<T>: DbContext where T : class, new()
{
    public DbSet<T> dbSet => Set<T>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("OfficeBaseAppDb");
    }
}