namespace OfficeBaseApp.Repositories;

using OfficeBaseApp.Entities;
using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Data;

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
        return _dbSet.Find(id);
    }

    public T? GetItem(string name)
    {
        var id = 0;
        foreach (var item in _dbSet)
        {
            if (item.Name == name)
            {
                id = item.Id;
            }
        }
        return _dbSet.Find(id);
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