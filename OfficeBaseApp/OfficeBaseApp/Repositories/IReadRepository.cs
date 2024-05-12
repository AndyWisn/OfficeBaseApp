using OfficeBaseApp.Entities;

namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;

public interface IReadRepository<out T> where T: class, IEntity
{
    IEnumerable<T> GetAll();
    T GetById(int id);
}

