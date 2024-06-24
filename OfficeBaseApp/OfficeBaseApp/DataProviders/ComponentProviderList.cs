using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
using System.Text;
using OfficeBaseApp.DataProviders.Extensions;

namespace OfficeBaseApp.DataProviders;

public class ComponentProviderList : IComponentProviderList
{
    private readonly IListRepository<Component> repository;

    public ComponentProviderList(IListRepository<Component> componentRepository)
    {
        repository = componentRepository;
    }

    public List<string> GetUniqueNames()
    {
        var components = repository.GetAll();
        var names = components.Select(x => x.Name).Distinct().ToList();
        return names;
    }
}

    //public List<string> GetUniqueComponentNames()
    //{
    //    var components = repository.GetAll();
    //    var names = components.Select(x => x.Name).Distinct().ToList();
    //    return names;
    //}

    //public ComponentProvider(ISqlRepository<Component> componentSqlRepository)
    //{
    //    _componentSqlRepository = componentSqlRepository;

    //}


    //public float GetMinimumPriceOfAllComponents()
    //{
    //    var components = repository.GetAll();
    //    return components.Select(x => x.Price).Min();
    //}

//    public List<string> GetUniqueNames()
//    {
//        var items = repository.GetAll();
//        var names = items.Select(x => x.Name).ToList();
//        return names;
//    }



//    public List<Component> GetSpecificColumns()
//    {
//        var components = repository.GetAll();
//        var list = components.Select(component => new Component
//        {
//            Name = component.Name,
//            Description = component.Description,
//            Price = component.Price
//        }).ToList();
//        return list;
//    }

//    public string AnonymousClass()
//    {
//        var components = repository.GetAll();
//        var list = components.Select(component => new
//        {
//            Identifier = component.Id,
//            ProductName = component.Name,
//            ProductType = component.Description
//        });

//        StringBuilder sb = new(2048);

//        foreach (var component in list)
//        {
//            sb.AppendLine($"Product ID: {component.Identifier}");
//            sb.AppendLine($"Product Name: {component.ProductName}");
//            sb.AppendLine($"Product Size: {component.ProductType}");
//        }

//        return sb.ToString();
//    }

//    public List<Component> OrderByName()
//    {
//        var components = repository.GetAll();
//        return components.OrderBy(x => x.Name).ToList();
//    }
//    public List<Component> OrderByNameDescending()
//    {
//        var components = repository.GetAll();
//        return components.OrderByDescending(x => x.Name).ToList();
//    }

//    public List<Component> OrderByNameAndPrice()
//    {
//        var components = repository.GetAll();
//        return components
//            .OrderBy(x => x.Price)
//            .ThenBy(x => x.Name)
//            .ToList();
//    }

//    public List<Component> OrderByNameAndPriceDesc()
//    {
//        var components = repository.GetAll();
//        return components
//            .OrderByDescending(x => x.Price)
//            .ThenByDescending(x => x.Name)
//            .ToList();
//    }

//    public List<Component> WhereStartsWith(string prefix)
//    {
//        var components = repository.GetAll();
//        return components.Where(x => x.Name.StartsWith(prefix)).ToList();

//    }
//    public List<Component> WhereStartsWithAndPriceIsGreatherThan(string prefix, float price)
//    {
//        var components = repository.GetAll();
//        return components.Where(x => x.Name.StartsWith(prefix) && x.Price > price).ToList();

//    }
//    public List<Component> WhereDescriptionIs(string description)
//    {
//        var components = repository.GetAll();
//        return components.ByDescription("AAA").ToList();
//    }

//    public Component FirstByDescription(string description)
//    {
//        var components = repository.GetAll();
//        return components.First(x => x.Description == description);

//    }
//    public Component? FirstOrDefaultByDescriptionWithDefault(string description)
//    {
//        var components = repository.GetAll();
//        return components.FirstOrDefault(x => x.Description == description, new Component { Id = -1, Name = "Not Found" });


//    }
//    public Component LastByDescription(string description)
//    {
//        var components = repository.GetAll();
//        return components.Last(x => x.Description == description);

//    }
//    public Component SingleById(int id)
//    {
//        var components = repository.GetAll();
//        return components.Single(x => x.Id == id);

//    }
//    public Component SingleOrDefualtById(int id)
//    {
//        var components = repository.GetAll();
//        return components.SingleOrDefault(x => x.Id == id, null);
//    }



//    public List<Component> TakeComponents(int howMany)
//    {
//        var components = repository.GetAll();
//        return components
//            .OrderBy(x => x.Name)
//            .Take(howMany)
//            .ToList();
//    }
//    public List<Component> TakeCars(Range range)
//    {
//        var components = repository.GetAll();
//        return components
//           .OrderBy(x => x.Name)
//           .Take(range)
//           .ToList();

//    }
//    public List<Component> TakeComponentsWhileNameStartsWith(string prefix)
//    {
//        var components = repository.GetAll();
//        return components
//           .OrderBy(x => x.Name)
//           .TakeWhile(x => x.Name.StartsWith(prefix))
//           .ToList();
//    }


//    public List<Component> SkipComponents(int howMany)
//    {
//        var components = repository.GetAll();
//        return components
//           .OrderBy(x => x.Name)
//           .Skip(howMany)
//           .ToList();

//    }

//    public List<Component> SkipComponentsWhileNameStartsWith(string prefix)
//    {
//        var components = repository.GetAll();
//        return components
//           .OrderBy(x => x.Name)
//           .SkipWhile(x => x.Name.StartsWith(prefix))
//           .ToList();
//    }


//    public List<string> DistinctAllNames()
//    {
//        var components = repository.GetAll();
//        return components
//           .Select(x => x.Name)
//           .Distinct()
//           .OrderBy(c => c)
//           .ToList();

//    }

//    public List<Component> DistinctByNames()
//    {
//        var components = repository.GetAll();
//        return components
//           .DistinctBy(x => x.Name)
//           .OrderBy(x => x.Name)
//           .ToList();
//    }

//    public List<Component[]> ChunkComponents(int size)  // zwraca listę tablic komponentów na której każda tablica jest paczką o rozmiarze size
//    {
//        var components = repository.GetAll();
//        return components.Chunk(size).ToList();
//    }

