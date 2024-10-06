using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp;
using OfficeBaseApp.Data;
using OfficeBaseApp.Components.DataProviders;
using OfficeBaseApp.Components.CsvReader;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using OfficeBaseApp.Components.XmlWriter;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddScoped<IRepository<Vendor>, SqlRepository<Vendor>>();
services.AddScoped<IRepository<ProductionPart>, SqlRepository<ProductionPart>>();
services.AddScoped<IRepository<Product>, SqlRepository<Product>>();
services.AddDbContext<OfficeBaseAppDbContext>(options =>
         options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestStorage;Integrated Security=True"));
services.AddSingleton<IProductionPartProvider, ProductionPartProvider>();
services.AddSingleton<IProductProvider, ProductProvider>();
services.AddSingleton<IVendorProvider, VendorProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlWriter, XmlWriter>();
var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();