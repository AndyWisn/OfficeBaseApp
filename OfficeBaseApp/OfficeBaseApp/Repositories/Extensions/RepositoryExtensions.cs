using Microsoft.IdentityModel.Tokens;
using OfficeBaseApp.Entities;

namespace OfficeBaseApp.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static void AddBatch<T>(this IRepository<T> repository, T[] items)
       where T : class, IEntity
    {
        foreach (var item in items)
        {
            repository.Add(item);
        }
        repository.Save();
    }

    public static void SetUp<T>(this IRepository<T> repository) where T : class, IEntity
    {
        repository.ItemAdded += RepositoryOnItemAdded;
        repository.ItemRemoved += RepositoryOnItemRemoved;
    }

    public static void RepositoryOnItemAdded<T>(object? sender, T e) where T : class, IEntity
    {
        using (var writer = File.AppendText("OfficeBaseApp_Backlog.TXT"))
        {
            if (!sender.GetType().Name.IsNullOrEmpty())
            {
                Console.WriteLine($"{e.GetType().Name} {e.Name} added from {sender?.GetType().Name.Remove(sender.GetType().Name.Length - 2)}");
                writer.WriteLine($"{DateTime.Now} ---> {e.GetType().Name} {e.Name} added to {sender?.GetType().Name.Remove(sender.GetType().Name.Length - 2)}");
            }
        }
    }

    public static void RepositoryOnItemRemoved<T>(object? sender, T e) where T : class, IEntity
    {
        using (var writer = File.AppendText("OfficeBaseApp_Backlog.TXT"))
        {
            if (!sender.GetType().Name.IsNullOrEmpty())
            {
                Console.WriteLine($"{e.GetType().Name} {e.Name} removed from {sender.GetType().Name.Remove(sender.GetType().Name.Length - 2)}");
                writer.WriteLine($"{DateTime.Now} ---> {e.GetType().Name} {e.Name} removed from {sender?.GetType().Name.Remove(sender.GetType().Name.Length - 2)}");
            }
        }
    }
    public static void WriteAllToConsole(this IReadRepository<IEntity> genericRepository)
    {
        var items = genericRepository.GetAll();
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

}