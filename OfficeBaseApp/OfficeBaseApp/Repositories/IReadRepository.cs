namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
public interface IReadRepository<out T> where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetItem(string name);
    T GetItem(int id);

}