using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Data.Repositories;

public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetItem(string name);
    T GetItem(int id);
}