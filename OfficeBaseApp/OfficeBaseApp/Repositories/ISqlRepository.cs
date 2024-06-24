namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;

public interface ISqlRepository<T> : IRepository<T> where T : class, IEntity
{
}