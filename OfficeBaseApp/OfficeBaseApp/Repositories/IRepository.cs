namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;
public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
    where T : class, IEntity
{
}