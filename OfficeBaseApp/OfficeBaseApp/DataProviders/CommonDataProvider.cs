﻿using OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
using System.Text;

namespace OfficeBaseApp.DataProviders;
public abstract class CommonDataProvider<T> : ICommonDataProvider<T> where T : class, IEntity, new()
{
    private readonly IRepository<T> _repository;
    protected CommonDataProvider(IRepository<T> repository)
    {
        _repository = repository;
    }
    public List<string> GetUniqueNames()
    {
        var components = _repository.GetAll();
        var names = components.Select(x => x.Name).Distinct().ToList();
        return names;
    }
    public List<T> OrderByName()
    {
        var items = _repository.GetAll();
        return items.OrderBy(x => x.Name).ToList();
    }
    public List<T> OrderByNameDescending()
    {
        var items = _repository.GetAll();
        return items.OrderByDescending(x => x.Name).ToList();
    }
    public List<T> WhereStartsWith(string prefix)
    {
        var components = _repository.GetAll();
        return components.Where(x => x.Name.StartsWith(prefix)).ToList();

    }
    public List<T> SkipItems(int howMany)
    {
        var components = _repository.GetAll();
        return components
           .OrderBy(x => x.Name)
           .Skip(howMany)
           .ToList();
    }
    public List<T> SkipItemsWhileNameStartsWith(string prefix)
    {
        var components = _repository.GetAll();
        return components
           .OrderBy(x => x.Name)
           .SkipWhile(x => x.Name.StartsWith(prefix))
           .ToList();
    }
    public List<string> DistinctAllNames()
    {
        var components = _repository.GetAll();
        return components
           .Select(x => x.Name)
           .Distinct()
           .OrderBy(c => c)
           .ToList();
    }
    public List<T> DistinctByNames()
    {
        var components = _repository.GetAll();
        return components
           .DistinctBy(x => x.Name)
           .OrderBy(x => x.Name)
           .ToList();
    }
    public List<T[]> ChunkItems(int size)
    {
        var components = _repository.GetAll();
        return components.Chunk(size).ToList();
    }
}