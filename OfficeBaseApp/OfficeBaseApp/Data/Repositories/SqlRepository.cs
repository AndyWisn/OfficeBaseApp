using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Data.Repositories;
public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly OfficeBaseAppDbContext _officeBaseAppDbContext;
    public SqlRepository(OfficeBaseAppDbContext officeBaseAppDbContext)
    {
        _officeBaseAppDbContext = officeBaseAppDbContext;
        _dbSet = _officeBaseAppDbContext.Set<T>();
    }
    public event EventHandler<T> ItemAdded;
    public event EventHandler<T> ItemRemoved;
    public void Add(T item)
    {
        _dbSet.Add(item);
        ItemAdded?.Invoke(this, item);
        Save();
        
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
        _officeBaseAppDbContext.SaveChanges();
    }
}