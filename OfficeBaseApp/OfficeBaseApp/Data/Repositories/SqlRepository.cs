using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Data;
using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Data.Repositories;
public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly OfficeBaseAppDbContext _dbContext;
    public SqlRepository(OfficeBaseAppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public event EventHandler<T> ItemAdded;
    public event EventHandler<T> ItemRemoved;
    public void Add(T item)
    {
        _dbSet.Add(item);
        Save();
        ItemAdded?.Invoke(this, item);
    }
    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }
    public T? GetItem(int id)
    {
        //return _dbSet.Find(id);

        return _dbSet.SingleOrDefault(x => x.Id == id, null);
    }
    public T? GetItem(string name)
    {
        return _dbSet.FirstOrDefault(x => x.Name == name, null);
    }
    public void Load()
    {
        using (var context = new OfficeBaseAppDbContext())
        {

            if (context.Database.CanConnect())
            {
                Console.WriteLine("Database connected.");
            }
            else if (context.Database.EnsureCreated())
            {
                Console.WriteLine("Database created.");
            }
        }
    }
    public void Remove(T item)
    {
        if (item != null)
        {
            ItemRemoved?.Invoke(this, item);
            _dbSet.Remove(item);
            Save();
        }
    }
    public void Save()
    {
        _dbContext.SaveChanges();
    }
}