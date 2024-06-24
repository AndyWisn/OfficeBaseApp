using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
using System.Text;
using OfficeBaseApp.DataProviders.Extensions;

namespace OfficeBaseApp.DataProviders;

public class ComponentProviderSql : IComponentProviderSql
{
    
    private readonly ISqlRepository<Component> _repository;
    
    public ComponentProviderSql(ISqlRepository<Component> componentRepository)
    {
        _repository = componentRepository;
    }

    public List<string> GetUniqueNames()
    {
        var components = _repository.GetAll();
        var names = components.Select(x => x.Name).Distinct().ToList();
        return names;
    }
}