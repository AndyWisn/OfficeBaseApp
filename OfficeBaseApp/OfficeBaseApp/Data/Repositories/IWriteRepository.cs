using OfficeBaseApp.Data.Entities;

namespace OfficeBaseApp.Data.Repositories;

public interface IWriteRepository<in T> where T : class, IEntity
{
    void Add(T item);
    void Remove(T item);
    void Save();
}