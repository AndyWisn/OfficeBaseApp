using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp;
using OfficeBaseApp.Entities;
using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
   
var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddScoped<IRepository<Customer>, SqlRepository<Customer>>();
services.AddScoped<IRepository<Vendor>, SqlRepository<Vendor>>();
services.AddScoped<IRepository<Component>, SqlRepository<Component>>();
services.AddScoped<IRepository<Product>, SqlRepository<Product>>();
services.AddDbContext<OfficeBaseAppDbContext>();
services.AddSingleton<IComponentProvider, ComponentProvider>();
var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();