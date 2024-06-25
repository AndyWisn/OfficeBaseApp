using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Repositories.Extensions;

namespace OfficeBaseApp;
public class App : IApp
{
    private readonly IListRepository<Customer> _customerListRepository;
    private readonly IListRepository<Vendor> _vendorListRepository;
    private readonly IListRepository<Component> _componentListRepository;
    private readonly IListRepository<Product> _productListRepository;

    private readonly ISqlRepository<Customer> _customerSqlRepository;
    private readonly ISqlRepository<Vendor> _vendorSqlRepository;
    private readonly ISqlRepository<Component> _componentSqlRepository;
    private readonly ISqlRepository<Product> _productSqlRepository;

    private readonly IComponentProviderSql _componentProviderSql;
    private readonly IComponentProviderList _componentProviderList;
    public App(IListRepository<Customer> customerRepository,
               IListRepository<Vendor> vendorRepository,
               IListRepository<Component> componentRepository,
               IListRepository<Product> productRepository,
               ISqlRepository<Customer> customerSqlRepository,
               ISqlRepository<Vendor> vendorSqlRepository,
               ISqlRepository<Component> componentSqlRepository,
               ISqlRepository<Product> productSqlRepository,
               IComponentProviderList componentProviderList,
               IComponentProviderSql componentProviderSql)
        
    {

        _customerListRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _customerListRepository.SetUp();
        _vendorListRepository = vendorRepository ?? throw new ArgumentNullException(nameof(vendorRepository));
        _vendorListRepository.SetUp();
        _componentListRepository = componentRepository ?? throw new ArgumentNullException(nameof(componentRepository));
        _componentListRepository.SetUp();
        _productListRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _productListRepository.SetUp();

        _customerSqlRepository = customerSqlRepository ?? throw new ArgumentNullException(nameof(customerSqlRepository));
        _customerSqlRepository.SetUp();
        _vendorSqlRepository = vendorSqlRepository ?? throw new ArgumentNullException(nameof(vendorSqlRepository));
        _vendorSqlRepository.SetUp();
        _componentSqlRepository = componentSqlRepository ?? throw new ArgumentNullException(nameof(componentSqlRepository));
        _componentSqlRepository.SetUp();
        _productSqlRepository = productSqlRepository ?? throw new ArgumentNullException(nameof(productSqlRepository));
        _productSqlRepository.SetUp();

        _componentProviderSql = componentProviderSql ?? throw new ArgumentNullException(nameof(componentProviderSql));
        _componentProviderList = componentProviderList ?? throw new ArgumentNullException(nameof(componentProviderList));
    }

    public void Run()


    {



        var menuObject = new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
                                      _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository,
                                      new List<string>(), new List<TextMenu.MenuItemAction>());

        menuObject.LoadRepository(_componentListRepository, 16);
        var items = _componentProviderList.GetUniqueNames();
        foreach (var item in items)
        {
            Console.WriteLine(item);

        }
        menuObject.WaitTillKeyPressed(true);
        Console.WriteLine();
        Console.WriteLine($"Minimum price of all Components {_componentProviderList.GetMinimumPriceOfAllComponents()}");
        menuObject.WaitTillKeyPressed(true);

        Console.WriteLine();
        Console.WriteLine("Get specific columns from all Components:");
        var items1 = _componentProviderList.GetSpecificColumns();
        foreach (var item in items1)
        {
            Console.WriteLine(item);

        }
        menuObject.WaitTillKeyPressed(true);

        Console.WriteLine();
        Console.WriteLine($"Anonymous class: "+_componentProviderList.AnonymousClass());
        menuObject.WaitTillKeyPressed(true);

        Console.WriteLine();
        Console.WriteLine("Components sorted by Name:");
        var items2 = _componentProviderList.OrderByName();
        foreach (var item in items2)
        {
            Console.WriteLine(item);

        }
        menuObject.WaitTillKeyPressed(true);

        Console.WriteLine();
        Console.WriteLine("Components sorted by Name, descending:");
        var items3 = _componentProviderList.OrderByNameDescending();
        foreach (var item in items3)
        {
            Console.WriteLine(item);

        }

        Console.WriteLine();
        Console.WriteLine("Components sorted by Price and then by Name:");
        var items4 = _componentProviderList.OrderByNameAndPrice();
        foreach (var item in items4)
        {
            Console.WriteLine(item);

        }

        menuObject.WaitTillKeyPressed(true);

        


        var menuItems = new List<string>() { "Exit", "Create/restore SQL Base seeded sample data", "Seed sample data to Json files", "SQL repository", "Memory Repository" };
        var menuActionMap = new List<TextMenu.MenuItemAction>()
        {
            () => {Console.Clear(); Environment.Exit(0);},
            () => menuObject.ResetDatabase(_customerSqlRepository,_vendorSqlRepository,_componentSqlRepository, _productSqlRepository, menuItems.Count + 6),
            () => menuObject.ResetJsonFiles(_customerListRepository,_vendorListRepository,_componentListRepository,_productListRepository, menuItems.Count + 6),
            menuObject.TextMenu_HandleSqlRepositorySelectMenu,
            menuObject.TextMenu_HandleListRepositorySelectMenu,
        };
        menuObject = new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
                                   _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository,
                                   menuItems, menuActionMap);
        menuObject.Run();

    }
}