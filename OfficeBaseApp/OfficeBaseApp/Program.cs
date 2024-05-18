using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using Microsoft.EntityFrameworkCore;

//Adding customers, vendors, Wholesalers (customer), Components & products to list repositories
var customerListRepository = new ListRepository<Customer>();
var vendorListRepository = new ListRepository<Vendor>();
var componentListRepository = new ListRepository<Component>();
var productListRepository = new ListRepository<Product>();

Console.WriteLine("\nTest List repositories:");
AddCustomers(customerListRepository);
AddWholesaler(customerListRepository);
WriteAllToConsole(customerListRepository);

AddVendor(vendorListRepository);
WriteAllToConsole(vendorListRepository);

AddComponent(componentListRepository, vendorListRepository);
WriteAllToConsole(componentListRepository);

AddProduct(productListRepository, componentListRepository);
WriteAllToConsole(productListRepository);


//Test item removing from list repositories
Console.WriteLine("\nTest List repositories after item remove:");
customerListRepository.Remove(customerListRepository.GetById(1));
customerListRepository.Remove(customerListRepository.GetById(2));
WriteAllToConsole(customerListRepository);

vendorListRepository.Remove(vendorListRepository.GetById(1));
vendorListRepository.Remove(vendorListRepository.GetById(2));
WriteAllToConsole(vendorListRepository);

componentListRepository.Remove(componentListRepository.GetById(1));
componentListRepository.Remove(componentListRepository.GetById(2));
productListRepository.GetById(1).RemoveProductionComponent(2);
productListRepository.Remove(productListRepository.GetById(2));
WriteAllToConsole(productListRepository);




//Adding customers, vendors, Wholesalers (customer), Components & products to Sql repositories
var customerRepository = new SqlRepository<Customer>(new OfficeBaseAppDbContext<Customer>());
var vendorRepository = new SqlRepository<Vendor>(new OfficeBaseAppDbContext<Vendor>());
var componentRepository = new SqlRepository<Component>(new OfficeBaseAppDbContext<Component>());
var productRepository = new SqlRepository<Product>(new OfficeBaseAppDbContext<Product>());

Console.WriteLine("\nTest EntityFramework Sql repositories:");
AddCustomers(customerRepository);
AddWholesaler(customerRepository);
WriteAllToConsole(customerRepository);

AddVendor(vendorRepository);
WriteAllToConsole(vendorRepository);

AddComponent(componentRepository, vendorRepository);
WriteAllToConsole(componentRepository);
AddProduct(productRepository, componentRepository);
WriteAllToConsole(productRepository);

//Test item removing from sql repositories
Console.WriteLine("\nTest Sql repositories after item remove:");
customerRepository.Remove(customerRepository.GetById(1));
customerRepository.Remove(customerRepository.GetById(2));
customerRepository.Save();
WriteAllToConsole(customerRepository);

vendorRepository.Remove(vendorRepository.GetById(1));
vendorRepository.Remove(vendorRepository.GetById(2));
vendorRepository.Save();
WriteAllToConsole(vendorRepository);

componentRepository.Remove(componentRepository.GetById(1));
componentRepository.Remove(componentRepository.GetById(2));
componentRepository.Save();
WriteAllToConsole(componentRepository);


productRepository.GetById(1).RemoveProductionComponent(2);
productRepository.Remove(productRepository.GetById(2));
productRepository.Save();
WriteAllToConsole(productRepository);



static void AddCustomers(IRepository<Customer> customerRepo)
{
    customerRepo.Add(new Customer ("AKG Industries A.G.","John", "Walker","+49123123123"));
    customerRepo.Add(new Customer ("Elemis Systems Inc.", "Ella","McKenzie", "+43123123123"));
    customerRepo.Add(new Customer ("F-Ine Corp", "Ian", "Femming","+44123123123"));
    customerRepo.Add(new Customer ("Microchip (UK)","Barry","Moore","+44123123123" ));
    customerRepo.Save();
}
static void AddVendor(IRepository<Vendor> vendorRepo)
{
    vendorRepo.Add(new Vendor ("Siemens A.G.","John","Ribena","+4256453123123","CE","+421564563123"));
    vendorRepo.Add(new Vendor ("Star Inc..","Cris","Cornell","+454233123123","CE, FCC","+45654423123" ));
    vendorRepo.Add(new Vendor ("Molex","Alan","Wider","+47532123123","CE, RoHS","+47133456423"));
    vendorRepo.Add(new Vendor("Mean-Well", "Ali", "Chang", "+47532123123", "CE, ,FCC, RoHS", "+4345656423"));
    vendorRepo.Save();
}
static void AddWholesaler(IRepository<Customer> customerRepo)
{
    customerRepo.Add(new Wholesaler("ABB Corp.","","","+482342634543"));
    customerRepo.Add(new Wholesaler("Dellscape","","","+481232594423"));
    customerRepo.Add(new Wholesaler("Fine Inc.","","","+481231563123"));
    customerRepo.Add(new Wholesaler("Rutimex","","", "+481223431123"));
    customerRepo.Save();
}
static void AddComponent(IRepository<Component> componentsRepo, IRepository<Vendor> vendorsRepo)
{    
    componentsRepo.Add(new Component("LM234",14.45f,"Converter"));
    componentsRepo.GetById(1).AddVendor(vendorsRepo.GetById(1));
    componentsRepo.Add(new Component("AXT435",23.43f,"Connector"));
    componentsRepo.GetById(2).AddVendor(vendorsRepo.GetById(2));
    componentsRepo.Add(new Component("IRM20-5",123.40f,"AC/DC module"));
    componentsRepo.GetById(3).AddVendor(vendorsRepo.GetById(1));
    componentsRepo.Add(new Component("CVB2423234",0.21f,"Filter"));
    componentsRepo.GetById(4).AddVendor(vendorsRepo.GetById(3));
    componentsRepo.Save();
}
static void AddProduct(IRepository<Product> productsRepo, IRepository<Component> componentRepo)
{
    productsRepo.Add(new Product("AKG1234PSU","AKG hi-voltage PSUv.34"));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(1));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(2));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(3));
    productsRepo.GetById(1).AddProductionComponent(componentRepo.GetById(4));
    productsRepo.Add(new Product("BKG2247PSU", "AKG lo-voltage PSUv.34"));
    productsRepo.GetById(2).AddProductionComponent(componentRepo.GetById(2));
    productsRepo.GetById(2).AddProductionComponent(componentRepo.GetById(3));
    productsRepo.GetById(2).AddProductionComponent(componentRepo.GetById(4));
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



