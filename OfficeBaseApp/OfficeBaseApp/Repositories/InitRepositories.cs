using OfficeBaseApp.Data;
using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Repositories.Extensions;
public class InitRepositories
{
    static public ListRepository<Customer> customerListRepository = new ListRepository<Customer>();
    static public ListRepository<Vendor> vendorListRepository = new ListRepository<Vendor>();
    static public ListRepository<Component> componentListRepository = new ListRepository<Component>();
    static public ListRepository<Product> productListRepository = new ListRepository<Product>();
    static public SqlRepository<Customer> customerRepository = new SqlRepository<Customer>(new OfficeBaseAppDbContext());
    static public SqlRepository<Vendor> vendorRepository = new SqlRepository<Vendor>(new OfficeBaseAppDbContext());
    static public SqlRepository<Component> componentRepository = new SqlRepository<Component>(new OfficeBaseAppDbContext());
    static public SqlRepository<Product> productRepository = new SqlRepository<Product>(new OfficeBaseAppDbContext());

    public static void Init()
    {   customerListRepository.SetUp();
        vendorListRepository.SetUp();
        componentListRepository.SetUp();
        productListRepository.SetUp();
        customerRepository.SetUp();
        vendorRepository.SetUp();
        componentRepository.SetUp();
        productRepository.SetUp();
    }
}