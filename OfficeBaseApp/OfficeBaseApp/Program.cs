using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;

var customerListRepository = new ListRepository<Customer>();
var vendorListRepository = new ListRepository<Vendor>();
var componentListRepository = new ListRepository<Component>();
var productListRepository = new ListRepository<Product>();


var customerRepository = new SqlRepository<Customer>(new OfficeBaseAppDbContext());
var vendorRepository = new SqlRepository<Vendor>(new OfficeBaseAppDbContext());
var componentRepository = new SqlRepository<Component>(new OfficeBaseAppDbContext());
var productRepository = new SqlRepository<Product>(new OfficeBaseAppDbContext());

//Adding customers, vendors, Wholesalers (customer), Components & products to list repositories
AddCustomers(customerListRepository);
AddVendor(vendorListRepository);
AddWholesaler(customerListRepository);
AddComponent(componentListRepository, vendorListRepository);
AddProduct(productListRepository, componentListRepository);

//Adding customers, vendors, Wholesalers (customer), Components & products to Sql repositories
AddCustomers(customerRepository);
AddVendor(vendorRepository);
AddWholesaler(customerRepository);
AddComponent(componentRepository, vendorRepository);
AddProduct(productRepository, componentRepository);

//Print List repositories
Console.WriteLine("\nTest List repositories:");
WriteAllToConsole(customerListRepository);
WriteAllToConsole(vendorListRepository);
WriteAllToConsole(componentListRepository);
WriteAllToConsole(productListRepository);

//Print SQL repositories
Console.WriteLine("\nTest EntityFramework Sql repositories:");
WriteAllToConsole(customerRepository);
WriteAllToConsole(vendorRepository);
WriteAllToConsole(componentRepository);
WriteAllToConsole(productRepository);


//Test item removing from list repositories
customerListRepository.Remove(customerListRepository.GetById(1));
customerListRepository.Remove(customerListRepository.GetById(2));
vendorListRepository.Remove(vendorListRepository.GetById(1));
vendorListRepository.Remove(vendorListRepository.GetById(2));
componentListRepository.Remove(componentListRepository.GetById(1));
componentListRepository.Remove(componentListRepository.GetById(2));
productListRepository.Remove(productListRepository.GetById(1));
Console.WriteLine("\nTest List repositories after item remove:");
WriteAllToConsole(customerListRepository);
WriteAllToConsole(vendorListRepository);
WriteAllToConsole(componentListRepository);
WriteAllToConsole(productListRepository);


//Test item removing from sql repositories
customerRepository.Remove(customerRepository.GetById(1));
customerRepository.Remove(customerRepository.GetById(2));
customerRepository.Save();

vendorRepository.Remove(vendorRepository.GetById(1));
vendorRepository.Remove(vendorRepository.GetById(2));
vendorRepository.Save();                                      //Błąd po save!

componentRepository.Remove(componentRepository.GetById(1));
componentRepository.Remove(componentRepository.GetById(2));
componentRepository.Save();

productRepository.Remove(productRepository.GetById(1));
productRepository.Save();

Console.WriteLine("\nTest EntityFramework Sql repositories after item remove:");
WriteAllToConsole(customerRepository);
WriteAllToConsole(vendorRepository);
WriteAllToConsole(componentRepository);
WriteAllToConsole(productRepository);


static void AddCustomers(IRepository<Customer> customerRepo)
{
    customerRepo.Add(new Customer { Name = "AKG Industries A.G.", RepresentativeFirstName = "John", RepresentativeLastName = "Walker", Contact = "+49123123123" });
    customerRepo.Add(new Customer { Name = "Elemis Systems Inc.", RepresentativeFirstName = "Ella", RepresentativeLastName = "McKenzie", Contact = "+43123123123" });
    customerRepo.Add(new Customer { Name = "F-Ine Corp", RepresentativeFirstName = "Ian", RepresentativeLastName = "Femming", Contact = "+44123123123" });
    customerRepo.Add(new Customer { Name = "Microchip (UK)", RepresentativeFirstName = "Barry", RepresentativeLastName = "Moore", Contact = "+44123123123" });
    //customerRepo.Save();
}

static void AddVendor(IRepository<Vendor> vendorRepo)
{
    vendorRepo.Add(new Vendor { Name = "Siemens A.G.", RepresentativeFirstName = "John", RepresentativeLastName = "Ribena", Contact = "+4256453123123", VendorCertificate = "CE", SupportContact = "+421564563123" });
    vendorRepo.Add(new Vendor { Name = "Star Inc..", RepresentativeFirstName = "Cris", RepresentativeLastName = "Cornell", Contact = "+454233123123", VendorCertificate = "CE", SupportContact = "+45654423123" });
    vendorRepo.Add(new Vendor { Name = "Molex", RepresentativeFirstName = "Alan", RepresentativeLastName = "Wider", Contact = "+47532123123", VendorCertificate = "CE", SupportContact = "+47133456423" });
    //vendorRepo.Save();
}

static void AddWholesaler(IRepository<Customer> customerRepo)
    {
    customerRepo.Add(new Wholesaler { Name = "ABB Corp.", Contact = "+482342634543" });
    customerRepo.Add(new Wholesaler { Name = "Dellscape", Contact = "+481232594423" });
    customerRepo.Add(new Wholesaler { Name = "Fine Inc.", Contact = "+481231563123" });
    customerRepo.Add(new Wholesaler { Name = "Rutimex", Contact = "+481223431123" });
    customerRepo.Save();
}

static void AddComponent(IRepository<Component> componentsRepo, IRepository<Vendor> vendorsRepo)
{
    componentsRepo.Add(new Component { Name = "LM234", ComponentsVendor = vendorsRepo.GetById(1), Price = 14.45f, Description = "Converter" });
    componentsRepo.Add(new Component { Name = "AXT435", ComponentsVendor = vendorsRepo.GetById(1), Price = 23.43f, Description = "Connector" });
    componentsRepo.Add(new Component { Name = "IRM20-5", ComponentsVendor = vendorsRepo.GetById(2), Price = 123.40f, Description = "AC/DC module" });
    componentsRepo.Add(new Component { Name = "CVB2423234", ComponentsVendor = vendorsRepo.GetById(3), Price = 0.21f, Description = "Filter" });
    componentsRepo.Save();
}

static void AddProduct(IRepository<Product> productsRepo, IRepository<Component> componentRepo)
{
    productsRepo.Add(new Product { Name = "AKG1234PSU", Description = "AKG hi-voltage PSUv.34" });
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(1));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(2));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(3));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(4));
    productsRepo.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> genericRepository)
{
    var items = genericRepository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}