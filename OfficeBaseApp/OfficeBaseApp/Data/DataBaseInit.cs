namespace OfficeBaseApp.Data;
using OfficeBaseApp.Entities;


public class DatabaseInit
{
    public static void Initialize()
    {
        using (var context = new OfficeBaseAppDbContext())
        {
            // Check if the database exists
            if (context.Database.CanConnect())
            {
                Console.WriteLine("Database exists.");
                context.Database.EnsureDeleted();
                Console.WriteLine("Database deleted.");
            }

            // Create the database
            context.Database.EnsureCreated();
            Console.WriteLine("New Database created.");

            //// Seed the database with demo data
            //if (!context.Customers.Any())
            //{
            //    context.Customers.AddRange
            //        (
            //         new Customer("AKG Industries A.G.", "John", "Walker", "+49123123123"),
            //         new Customer("Elemis Systems Inc.", "Ella", "McKenzie", "+43123123123"),
            //         new Customer("F-Ine Corp", "Ian", "Femming", "+44123123123"),
            //         new Customer("Microchip (UK)", "Barry", "Moore", "+44123123123")
            //        );
            //}

            //if (!context.Vendors.Any())
            //{
            //    context.Vendors.AddRange
            //        (
            //        new Vendor("Siemens A.G.", "John", "Ribena", "+4256453123123", "CE", "+421564563123"),
            //        new Vendor("Star Inc..", "Cris", "Cornell", "+454233123123", "CE, FCC", "+45654423123"),
            //        new Vendor("Molex", "Alan", "Wider", "+47532123123", "CE, RoHS", "+47133456423"),
            //        new Vendor("Mean-Well", "Ali", "Chang", "+47532123123", "CE, ,FCC, RoHS", "+4345656423")
            //        );
            //}

            //if (!context.Components.Any())
            //{
            //    context.Components.AddRange
            //        (
            //        new Component("LM234", 14.45f, "Converter", 1),
            //        new Component("AXT435", 23.43f, "Connector", 2),
            //        new Component("IRM20-5", 123.40f, "AC/DC module", 3),
            //        new Component("CVB2423234", 0.21f, "Filter", 4)
            //        );
            //}

            //if (!context.Products.Any())
            //{
            //    context.Products.AddRange
            //        (
            //        new Product("AKG1234PSU", "AKG hi-voltage PSU", new List<int> { 1, 2, 3, 4 }),
            //        new Product("BKG2247PSU", "MLV lo-voltage PSU", new List<int> { 1, 2, 2, 3 }),
            //        new Product("BKG2247PSU", "BSS mid-buffer PSU", new List<int> { 1, 2, 3, 3 })
            //        );
            //}

            context.SaveChanges();
            Console.WriteLine("Database seeded with demo data.");
        }
    }
}