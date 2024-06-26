﻿using System.Text;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;

namespace OfficeBaseApp.Components.DataProviders;
public class ComponentProvider : DataProvider<Component>, IComponentProvider
{
    private readonly IRepository<Component> _repository;
    public ComponentProvider(IRepository<Component> repository) : base(repository)
    {
        _repository = repository;
    }
    public float GetMinimumPriceOfAllComponents()
    {
        var components = _repository.GetAll();
        return components.Select(x => x.Price).Min();
    }
    public List<Component> GetSpecificColumns()
    {
        var components = _repository.GetAll();
        var list = components.Select(component => new Component
        {
            Name = component.Name,
            Description = component.Description,
            Price = component.Price
        }).ToList();
        return list;
    }
    public string AnonymousClass()
    {
        var components = _repository.GetAll();
        var list = components.Select(component => new
        {
            Identifier = component.Id,
            ProductName = component.Name,
            ProductType = component.Description
        });
        StringBuilder sb = new(2048);
        foreach (var component in list)
        {
            sb.AppendLine($"Product ID: {component.Identifier}");
            sb.AppendLine($"Product Name: {component.ProductName}");
            sb.AppendLine($"Product Size: {component.ProductType}");
        }
        return sb.ToString();
    }
    public List<Component> OrderByNameAndPrice()
    {
        var components = _repository.GetAll();
        return components
            .OrderBy(x => x.Price)
            .ThenBy(x => x.Name)
            .ToList();
    }
}