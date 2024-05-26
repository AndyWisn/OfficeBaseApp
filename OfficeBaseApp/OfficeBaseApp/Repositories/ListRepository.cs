namespace OfficeBaseApp.Repositories;

using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Entities;
using System.Reflection;
using System.Text.Json;

public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
{

    private readonly List<T> _items = new();

    public event EventHandler<T> ItemAdded;
    public event EventHandler<T> ItemRemoved;

    private string FileName = null;


  
    public IEnumerable<T> GetAll()
    {
        return _items.ToList();
    }
    public void Add(T item)
    {
        item.Id = _items.Count + 1;
        _items.Add(item);
        ItemAdded?.Invoke(this, item);
    }
    public T? GetItem(int id)
    {
        return _items[id - 1];
    }

    public T? GetItem(string name)
    {
        var id = 0;
        foreach (var item in _items)
        {
            if (item.Name == name)
            {
                id = item.Id;
            }
        }
        return _items[id - 1];
    }
    public void Remove(T item)
    {
        _items.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }
    public void LoadJson()
    {
        this.FileName = typeof(T).Name + "ListRepository.json";
        if (File.Exists(FileName))
        {
            using (var reader = File.OpenText(FileName))
            {
                string? json = null;
                while ((json=reader.ReadLine()) != null)
                {
                   Add(JsonSerializer.Deserialize<T>(json));
                }
            }
        }
    }
    public void Save() 
    {
        if (_items.Count > 0)
        {
            this.FileName = _items.First().GetType().Name + "ListRepository.json";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"==> Saving file: {FileName}");
            using (var writer = File.AppendText(FileName))
            {
                foreach (var item in _items)
                {
                    var json = JsonSerializer.Serialize<T>(item);
                    writer.WriteLine(json);
                }
             }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
        }

    }            
}