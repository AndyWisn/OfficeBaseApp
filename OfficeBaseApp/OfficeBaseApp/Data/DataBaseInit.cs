namespace OfficeBaseApp.Data;
public class DatabaseInit
{
    public static void Initialize()
    {
        using (var context = new OfficeBaseAppDbContext())
        {
            if (context.Database.CanConnect())
            {
                Console.WriteLine("Database exists.");
                context.Database.EnsureDeleted();
                Console.WriteLine("Database deleted.");
            }
            context.Database.EnsureCreated();
            Console.WriteLine("New Database created.");
            context.SaveChanges();
            Console.WriteLine("Database seeded with demo data.");
        }
    }
}