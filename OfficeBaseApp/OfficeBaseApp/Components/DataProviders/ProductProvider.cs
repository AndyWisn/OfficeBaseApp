using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;

namespace OfficeBaseApp.Components.DataProviders;
public class ProductProvider : GenericDataProvider<Product>, IProductProvider
{
    private readonly IRepository<Product> _repository;
    public ProductProvider(IRepository<Product> repository) : base(repository)
    {
        _repository = repository;
    }
}