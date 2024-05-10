using OfficeBaseApp.Entities;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;

var customersRepository = new ListRepository<Customer>();

//customersRepository.Add(new Customer { Name = "Andy", Contact = "+48123123123" });
//customersRepository.Add(new Customer { Name = "Beth", Contact = "+48123123123" });
//customersRepository.Add(new Customer { Name = "Adam", Contact = "+48123123123" });
//customersRepository.Save();

var sqlRepository = new SqlRepository(new OfficeBaseAppDbContext());
sqlRepository.Add(new Customer { Name = "Andy", Contact = "+48123123123" });
sqlRepository.Add(new Customer { Name = "Beth", Contact = "+48123123123" });
sqlRepository.Add(new Customer { Name = "Adam", Contact = "+48123123123" });
sqlRepository.Add(new Customer { Name = "Jack", Contact = "+48123123123" });
sqlRepository.Save();
var cust = sqlRepository.GetById(4);
Console.WriteLine(cust.ToString());



