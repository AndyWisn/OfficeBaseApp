using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Repositories.Extensions;
using static OfficeBaseApp.TextMenu;

namespace OfficeBaseApp;

public class App : IApp
{
    private static ListRepository<Customer> _customerListRepository;
    private static ListRepository<Vendor> _vendorListRepository;
    private static ListRepository<Component> _componentListRepository;
    private static ListRepository<Product> _productListRepository;

    private static SqlRepository<Customer> _customerSqlRepository;
    private static SqlRepository<Vendor> _vendorSqlRepository;
    private static SqlRepository<Component> _componentSqlRepository;
    private static SqlRepository<Product> _productSqlRepository;

    public App(ListRepository<Customer> customerListRepository,
                    ListRepository<Vendor> vendorListRepository,
                    ListRepository<Component> componentListRepository,
                    ListRepository<Product> productListRepository,
                    SqlRepository<Customer> customerSqlRepository,
                    SqlRepository<Vendor> vendorSqlRepository,
                    SqlRepository<Component> componentSqlRepository,
                    SqlRepository<Product> productSqlRepository)
    {
        _customerListRepository = customerListRepository;
        _vendorListRepository = vendorListRepository;
        _componentListRepository = componentListRepository;
        _productListRepository = productListRepository;
        _customerSqlRepository = customerSqlRepository;
        _vendorSqlRepository = vendorSqlRepository;
        _componentSqlRepository = componentSqlRepository;
        _productSqlRepository = productSqlRepository;
        _customerListRepository.SetUp();
        _vendorListRepository.SetUp();
        _componentListRepository.SetUp();
        _productListRepository.SetUp();
        _customerSqlRepository.SetUp();
        _vendorSqlRepository.SetUp();
        _componentSqlRepository.SetUp();
        _productSqlRepository.SetUp();
    }

    public void Run()
    {
        PrintHeader();
        var menuItems = new List<string>() { "Exit", "Create/restore SQL Base seeded sample data", "Seed sample data to Json files", "SQL repository", "Memory Repository" };
        var menuActionMap = new List<MenuItemAction>()
        {
            () => {Console.Clear(); Environment.Exit(0);},
            () => ResetDatabase(_customerSqlRepository, _vendorSqlRepository, _componentSqlRepository, _productSqlRepository, menuItems.Count + 6),
            () => ResetJsonFiles(_customerListRepository, _vendorListRepository, _componentListRepository, _productListRepository, menuItems.Count + 6),
            HandleSqlRepositorySelectMenu,
            HandleListRepositorySelectMenu,
        };
        new TextMenu(menuItems, menuActionMap).Run();
    }

    private static void HandleSqlRepositorySelectMenu()
    {
        PrintHeader();
        var subMenu3Items = new List<string> { "<-Back", "Customers", "Vendors", "Components", "Products" };
        var subMenu3ActionMap = new List<MenuItemAction>()
                { () => {},
                  () => {var subMenu31Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest" };
                         var subMenu31ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadSqlRepository(subMenu31Items.Count + 6),
                             () => PrintRepository(_customerSqlRepository, subMenu31Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu31Items.Count + 6);
                                _customerSqlRepository.Add(new Customer().EnterPropertiesFromConsole());
                                PrintRepository(_customerSqlRepository, subMenu31Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_customerSqlRepository, subMenu31Items.Count + 6);},
                             () => { }
                            };
                        new TextMenu(subMenu31Items, subMenu31ActionMap).Run();

                        },

                  () => {var subMenu32Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu32ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadSqlRepository(subMenu32Items.Count + 6),
                             () => PrintRepository(_vendorSqlRepository, subMenu32Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu32Items.Count + 6);
                                 _vendorSqlRepository.Add(new Vendor().EnterPropertiesFromConsole());
                                PrintRepository(_vendorSqlRepository, subMenu32Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_vendorSqlRepository, subMenu32Items.Count + 6);},
                             () => { }
                            };
                         new TextMenu(subMenu32Items, subMenu32ActionMap).Run();

                         },

                  () => {var subMenu33Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu33ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadSqlRepository(subMenu33Items.Count + 6),
                             () => PrintRepository(_componentSqlRepository, subMenu33Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu33Items.Count + 6);
                                _componentSqlRepository.Add(new Component().EnterPropertiesFromConsole());
                                PrintRepository(_componentSqlRepository, subMenu33Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_vendorSqlRepository, subMenu33Items.Count + 6);},
                             () => { }
                            };
                        new TextMenu(subMenu33Items, subMenu33ActionMap).Run();

                         },

                  () => {var subMenu34Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu34ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadSqlRepository(subMenu34Items.Count + 6),
                             () => PrintRepository(_productSqlRepository, subMenu34Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu34Items.Count + 6);
                                _productSqlRepository.Add(new Product().EnterPropertiesFromConsole());
                                PrintRepository(_componentSqlRepository, subMenu34Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_productSqlRepository, subMenu34Items.Count + 6);},
                             () => { }
                            };
                        new TextMenu(subMenu34Items, subMenu34ActionMap).Run();

                        },
                };
        new TextMenu(subMenu3Items, subMenu3ActionMap).Run();
    }

    private static void HandleListRepositorySelectMenu()
    {
        PrintHeader();
        var subMenu4Items = new List<string> { "<-Back", "Customers", "Vendors", "Components", "Products" };
        var subMenu4ActionMap = new List<MenuItemAction>()
                { () => {},
                  () => {var subMenu41Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu41ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadListRepository(_customerListRepository, subMenu41Items.Count + 6),
                             () => PrintRepository(_customerListRepository, subMenu41Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu41Items.Count + 6);
                                _customerListRepository.Add(new Customer().EnterPropertiesFromConsole());
                                PrintRepository(_customerListRepository, subMenu41Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_customerListRepository, subMenu41Items.Count + 6);},
                             () => {}
                            };
                        new TextMenu(subMenu41Items, subMenu41ActionMap).Run();

                        },

                  () => {var subMenu42Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu42ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadListRepository(_vendorListRepository, subMenu42Items.Count + 6),
                             () => PrintRepository(_vendorListRepository, subMenu42Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu42Items.Count + 6);
                                _vendorListRepository.Add(new Vendor().EnterPropertiesFromConsole());
                                PrintRepository(_vendorListRepository, subMenu42Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_vendorListRepository, subMenu42Items.Count + 6);},
                             () => { }
                            };
                         new TextMenu(subMenu42Items, subMenu42ActionMap).Run();
                         },

                  () => {var subMenu43Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu43ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadListRepository(_componentListRepository, subMenu43Items.Count + 6),
                             () => PrintRepository(_componentListRepository, subMenu43Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu43Items.Count + 6);
                                _componentListRepository.Add(new Component().EnterPropertiesFromConsole());
                                PrintRepository(_componentListRepository, subMenu43Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_vendorListRepository, subMenu43Items.Count + 6);},
                             () => { }
                            };
                        new TextMenu(subMenu43Items, subMenu43ActionMap).Run();
                         },

                  () => {var subMenu44Items=new List<string>  {"<-Back", "Load", "Print", "Add", "Remove", "DataProviderTest"};
                         var subMenu44ActionMap= new List<MenuItemAction>()
                            {() => { },
                             () => LoadListRepository(_productListRepository, subMenu44Items.Count + 6),
                             () => PrintRepository(_productListRepository, subMenu44Items.Count + 6),
                             () => {
                                 Console.SetCursorPosition(0,subMenu44Items.Count + 6);
                                _productListRepository.Add(new Product().EnterPropertiesFromConsole());
                                PrintRepository(_componentListRepository, subMenu44Items.Count + 6);
                                WaitTillKeyPressed(true);
                                },
                             () => {RemoveItemFromRepository(_productListRepository, subMenu44Items.Count + 6);},
                             () => { }
                            };
                        new TextMenu(subMenu44Items, subMenu44ActionMap).Run();
                        },
                };
        new TextMenu(subMenu4Items, subMenu4ActionMap).Run();
    }
}