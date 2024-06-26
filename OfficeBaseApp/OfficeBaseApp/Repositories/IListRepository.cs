namespace OfficeBaseApp.Repositories;
using OfficeBaseApp.Entities;

public interface IListRepository<T> : IRepository<T> where T : class, IEntity
{
    public void DeleteJsonFiles();
}