namespace OfficeBaseApp.Data.Repositories;

using OfficeBaseApp.Data.Entities;
public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetItem(string name);
    T GetItem(int id);
}