using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp;
using OfficeBaseApp.Entities;
using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;
   
var services = new ServiceCollection();
services.AddSingleton<IApp, App>();


services.AddSingleton<IListRepository<Customer>, ListRepository<Customer>>();
services.AddSingleton<IListRepository<Vendor>, ListRepository<Vendor>>();
services.AddSingleton<IListRepository<Component>, ListRepository<Component>>();
services.AddSingleton<IListRepository<Product>, ListRepository<Product>>();

services.AddScoped<ISqlRepository<Customer>, SqlRepository<Customer>>();
services.AddScoped<ISqlRepository<Vendor>, SqlRepository<Vendor>>();
services.AddScoped<ISqlRepository<Component>, SqlRepository<Component>>();
services.AddScoped<ISqlRepository<Product>, SqlRepository<Product>>();

services.AddDbContext<OfficeBaseAppDbContext>();

services.AddSingleton<IComponentProviderList, ComponentProviderList>();
services.AddSingleton<IComponentProviderSql, ComponentProviderSql>();



var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();