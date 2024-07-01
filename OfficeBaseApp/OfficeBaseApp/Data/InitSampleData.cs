namespace OfficeBaseApp.Data;

using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;
using OfficeBaseApp.Data.Repositories.Extensions;

internal class InitSampleData
{   
    internal static void AddCustomers(IRepository<Customer> customerRepo)
    {
        Customer[] customers = new[]
        {
                new Customer("AKG Industries A.G.", "John", "Walker", "+49123123123"),
                new Customer("Elemis Systems Inc.", "Ella", "McKenzie", "+43123123123"),
                new Customer("F-Ine Corp", "Ian", "Femming", "+44123123123"),
                new Customer("Microchip (UK)", "Barry", "Moore", "+44123123123")
        };
        customerRepo.AddBatch(customers);
    }
    internal static void AddVendors(IRepository<Vendor> vendorRepo)
    {
        Vendor[] vendors = new[]
       {
                new Vendor("Siemens A.G.", "John", "Ribena", "+4256453123123", "CE", "+421564563123"),
                new Vendor("Star Inc..", "Cris", "Cornell", "+454233123123", "CE, FCC", "+45654423123"),
                new Vendor("Molex", "Alan", "Wider", "+47532123123", "CE, RoHS", "+47133456423"),
                new Vendor("Mean-Well", "Ali", "Chang", "+47532123123", "CE, ,FCC, RoHS", "+4345656423")
        };
        vendorRepo.AddBatch(vendors);
    }
   
    internal static void AddComponents(IRepository<Component> componentsRepo)
    {
        Component[] components = new[]
        {
            new Component("LM234", 14.45f, "Converter", 1),
            new Component("AXT435", 23.43f, "Connector", 2),
            new Component("IRM20-5", 123.40f, "AC/DC module", 3),
            new Component("CVB2423234", 0.21f, "Filter", 4)
        };
        componentsRepo.AddBatch(components);
    }
    internal static void AddProducts(IRepository<Product> productsRepo)
    {
        Product[] products = new[]
        {
            new Product("AKG1234PSU", "AKG hi-voltage PSU", new List<int>{ 1, 2, 3, 4 }),
            new Product("BKG2247PSU", "MLV lo-voltage PSU", new List<int>{ 1, 2, 2, 3 }),
            new Product("BKG2247PSU", "BSS mid-buffer PSU", new List<int>{ 1, 2, 3, 3 })
        };
        productsRepo.AddBatch(products);
    }
}