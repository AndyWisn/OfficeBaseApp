using System.Text;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;

namespace OfficeBaseApp.Components.DataProviders;
public class ProductionPartProvider : GenericDataProvider<ProductionPart>, IProductionPartProvider
{
    private readonly IRepository<ProductionPart> _repository;
    public ProductionPartProvider(IRepository<ProductionPart> repository) : base(repository)
    {
        _repository = repository;
    }
    public float GetMinimumPriceOfAllParts()
    {
        var components = _repository.GetAll();
        return components.Select(x => x.Price).Min();
    }
    public List<ProductionPart> GetSpecificColumns()
    {
        var components = _repository.GetAll();
        var list = components.Select(component => new ProductionPart
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
        StringBuilder sb = new(1024);
        foreach (var component in list)
        {
            sb.AppendLine($"Product ID: {component.Identifier}");
            sb.AppendLine($"Product Name: {component.ProductName}");
            sb.AppendLine($"Product Type: {component.ProductType}");
        }
        return sb.ToString();
    }
    public List<ProductionPart> OrderByNameAndPrice()
    {
        var components = _repository.GetAll();
        return components.OrderBy(x => x.Price)
                         .ThenBy(x => x.Name)
                         .ToList();
    }
}