using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
using Microsoft.EntityFrameworkCore;

var customerListRepository = new ListRepository<Customer>();
var vendorListRepository = new ListRepository<Vendor>();
var componentListRepository = new ListRepository<Component>();
var productListRepository = new ListRepository<Product>();

//Adding customers, vendors, Wholesalers (customer), Components & products to list repositories

AddCustomers(customerListRepository);
AddVendor(vendorListRepository);
AddWholesaler(customerListRepository);
AddComponent(componentListRepository, vendorListRepository);
AddProduct(productListRepository, componentListRepository);

//Print List repositories
Console.WriteLine("\nTest List repositories:");
WriteAllToConsole(customerListRepository);
WriteAllToConsole(vendorListRepository);
WriteAllToConsole(componentListRepository);
WriteAllToConsole(productListRepository);

//Test item removing from list repositories
customerListRepository.Remove(customerListRepository.GetById(1));
customerListRepository.Remove(customerListRepository.GetById(2));
vendorListRepository.Remove(vendorListRepository.GetById(1));
vendorListRepository.Remove(vendorListRepository.GetById(2));
componentListRepository.Remove(componentListRepository.GetById(1));
componentListRepository.Remove(componentListRepository.GetById(2));
productListRepository.GetById(1).RemoveProductionComponent(4));
//productListRepository.Remove(productListRepository.GetById(1));

Console.WriteLine("\nTest List repositories after item remove:");
WriteAllToConsole(customerListRepository);
WriteAllToConsole(vendorListRepository);
WriteAllToConsole(componentListRepository);
WriteAllToConsole(productListRepository);





//var customerRepository = new SqlRepository<Customer>(new OfficeBaseAppDbContext<Customer>());
//var vendorRepository = new SqlRepository<Vendor>(new OfficeBaseAppDbContext<Vendor>());
//var componentRepository = new SqlRepository<Component>(new OfficeBaseAppDbContext<Component>());
//var productRepository = new SqlRepository<Product>(new OfficeBaseAppDbContext<Product>());

//Adding customers, vendors, Wholesalers (customer), Components & products to Sql repositories
//AddCustomers(customerRepository);
//AddVendor(vendorRepository);
//AddWholesaler(customerRepository);
//AddComponent(componentRepository, vendorRepository);
//AddProduct(productRepository, componentRepository);

//Print SQL repositories
//Console.WriteLine("\nTest EntityFramework Sql repositories:");
//WriteAllToConsole(customerRepository);
//WriteAllToConsole(vendorRepository);
//WriteAllToConsole(componentRepository);
//WriteAllToConsole(productRepository);


////Test item removing from sql repositories
//customerRepository.Remove(customerRepository.GetById(1));
//customerRepository.Remove(customerRepository.GetById(2));
//customerRepository.Save();

//vendorRepository.Remove(vendorRepository.GetById(1));
//vendorRepository.Remove(vendorRepository.GetById(2));
//vendorRepository.Save();

//componentRepository.Remove(componentRepository.GetById(1));
//componentRepository.Remove(componentRepository.GetById(2));
//componentRepository.Save();

//productRepository.Remove(productRepository.GetById(1));
//productRepository.Save();

//Console.WriteLine("\nTest EntityFramework Sql repositories after item remove:");
//WriteAllToConsole(customerRepository);
//WriteAllToConsole(vendorRepository);
//WriteAllToConsole(componentRepository);
//WriteAllToConsole(productRepository);





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
    
    componentsRepo.Add(new Component("LM234",vendorsRepo.GetById(1),14.45f,"Converter" ));
    componentsRepo.Add(new Component("AXT435",vendorsRepo.GetById(2),23.43f,"Connector" ));
    componentsRepo.Add(new Component("IRM20-5",vendorsRepo.GetById(2),123.40f,"AC/DC module"));
    componentsRepo.Add(new Component("CVB2423234",vendorsRepo.GetById(3),0.21f,"Filter" ));
    componentsRepo.Save();
}

static void AddProduct(IRepository<Product> productsRepo, IRepository<Component> componentRepo)
{
    productsRepo.Add(new Product("AKG1234PSU","AKG hi-voltage PSUv.34"));
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