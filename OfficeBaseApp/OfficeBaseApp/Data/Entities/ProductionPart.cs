﻿namespace OfficeBaseApp.Data.Entities;
public class ProductionPart : IEntity
{
    public ProductionPart()
    {
    }

    public ProductionPart(string name, float price, string description, string partVendor, string partManufacturer)
    {
        Name = name;
        Description = description;
        Price = price;
        PartManufacturer = partManufacturer;
        PartVendor = partVendor;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PartManufacturer { get; set; }
    public string? Description { get; set; }
    public float Price { get; set; }
    public string? PartVendor { get; set; }

    public void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Production Part to repository:");
        Console.WriteLine();
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter Manufacturer:");
        PartManufacturer = Console.ReadLine();
        float price;
        do
        {
            Console.WriteLine("Enter price:");
        }
        while (!float.TryParse(Console.ReadLine(), out price));
        Price = price;
        Console.WriteLine("Enter desciption:");
        Description = Console.ReadLine();
        Console.WriteLine("Enter vendor:");
        PartVendor = Console.ReadLine();
        Console.CursorVisible = false;
    }

    public override string ToString() => string.Format("{0,-5} {1,-30} {2,-40} {3,-8} {4,-10}", Id, Name, PartManufacturer, Price, PartVendor);
}