using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;

//var customersRepository = new ListRepository<Customer>();


var customersRepository = new SqlRepository<Customer>(new OfficeBaseAppDbContext());
var vendorsRepository = new SqlRepository<Vendor>(new OfficeBaseAppDbContext());

AddCustomers(customersRepository);
AddVendor(vendorsRepository);
AddWholesaler(customersRepository);

WriteAllToConsole(customersRepository);
//WriteAllToConsole(vendorsRepository);

//Console.WriteLine("{0,-10} {1,5}\n", "Name", "Hours");






static void AddCustomers(IRepository<Customer> customersRepository)
{ 
    customersRepository.Add(new Customer { Name = "AKG Industries A.G.", RepresentativeFirstName="John", RepresentativeLastName="Walker", Contact = "+49123123123"});
    customersRepository.Add(new Customer { Name = "Elemis Systems Inc.", RepresentativeFirstName="Ella", RepresentativeLastName="McKenzie", Contact = "+43123123123" });
    customersRepository.Add(new Customer { Name = "F-Ine Corp", RepresentativeFirstName = "Ian", RepresentativeLastName = "Femming",Contact = "+44123123123" });
    customersRepository.Add(new Customer { Name = "Microchip (UK)", RepresentativeFirstName = "Barry", RepresentativeLastName = "Moore", Contact = "+44123123123" });
    customersRepository.Save();
}

static void AddVendor(IRepository<Vendor> vendorsRepository)
{
    vendorsRepository.Add(new Vendor { Name = "John", Contact = "+48234234543" });
    vendorsRepository.Add(new Vendor { Name = "Leo", Contact = "+48123254423" });
    vendorsRepository.Add(new Vendor { Name = "Nell", Contact = "+481231563123" });
    vendorsRepository.Add(new Vendor { Name = "Jimi", Contact = "+481223431123" });
    vendorsRepository.Save();
}

static void AddWholesaler(IRepository<Customer> customersRepository)
{
    customersRepository.Add(new Wholesaler { Name = "Amy", Contact = "+48234234543" });
    customersRepository.Add(new Wholesaler { Name = "Josh", Contact = "+48123254423" });
    customersRepository.Add(new Wholesaler { Name = "Beth", Contact = "+481231563123" });
    customersRepository.Add(new Wholesaler { Name = "Leia", Contact = "+481223431123" });
    customersRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> genericRepository)
{
    var items = genericRepository.GetAll();
    foreach(var item in items)
    {
        Console.WriteLine(item);
    }

}





