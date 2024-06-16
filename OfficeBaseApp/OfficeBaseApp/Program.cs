using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp;
using OfficeBaseApp.Entities;
using OfficeBaseApp.DataProviders;
using OfficeBaseApp.Repositories;
using OfficeBaseApp.Data;

var services = new ServiceCollection();

services.AddSingleton<ITextMenu, TextMenu>();

services.AddSingleton<ListRepository<Customer>, ListRepository<Customer>>();
services.AddSingleton<ListRepository<Vendor>, ListRepository<Vendor>>();
services.AddSingleton<ListRepository<Component>, ListRepository<Component>>();
services.AddSingleton<ListRepository<Product>, ListRepository<Product>>();

services.AddDbContext<OfficeBaseAppDbContext>();
services.AddScoped<SqlRepository<Customer>, SqlRepository<Customer>>();
services.AddScoped<SqlRepository<Vendor>, SqlRepository<Vendor>>();
services.AddScoped<SqlRepository<Component>, SqlRepository<Component>>();
services.AddScoped<SqlRepository<Product>, SqlRepository<Product>>();

services.AddSingleton<IComponentProvider, ComponentProvider>();

var serviceProvider = services.BuildServiceProvider();
var textMenu= serviceProvider.GetService<ITextMenu>()!;
textMenu.Run();