namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using OfficeBaseApp.Data;

public class SqlRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
    private readonly DbSet<T> _dbSet;
    private readonly OfficeBaseAppDbContext<T> _dbContext;
    public SqlRepository(OfficeBaseAppDbContext<T> dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public void Add(T item)
    {
       _dbSet.Add(item);
    }
    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }
    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }
    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }
    public async void Save()
    {
        await _dbContext.SaveChangesAsync();
    }     
}