using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp.Data;
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
        _customerListRepository = customerRepository;
        _customerListRepository.SetUp();
        _vendorListRepository = vendorRepository;
        _vendorListRepository.SetUp();
        _componentListRepository = componentRepository;
        _componentListRepository.SetUp();
        _productListRepository = productRepository;
        _productListRepository.SetUp();

        _customerSqlRepository = customerSqlRepository;
        _customerSqlRepository.SetUp();
        _vendorSqlRepository = vendorSqlRepository;
        _vendorSqlRepository.SetUp();
        _componentSqlRepository = componentSqlRepository;
        _componentSqlRepository.SetUp();
        _productSqlRepository = productSqlRepository;
        _productSqlRepository.SetUp();

        _componentProviderSql = componentProviderSql;
        _componentProviderList = componentProviderList;
        }

    public void Run()
    {
        var menuObject = new TextMenu(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository,
                                      _customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository, 
                                      _componentProviderList, _componentProviderSql,
                                      new List<string>(), new List<TextMenu.MenuItemAction>());

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
                                   _componentProviderList, _componentProviderSql,
                                   menuItems, menuActionMap);
        menuObject.Run();
    }
}