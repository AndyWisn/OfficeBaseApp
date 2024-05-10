namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
using Microsoft.EntityFrameworkCore;

public class ListRepository<T> where T : class, IEntity, new()
{
    private readonly List<T> _items = new();
    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }

    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
    }
    public T? GetById(int id)
    {
        return default(T);
    }
    public void Save()
    {
       //not required with list
    }
}