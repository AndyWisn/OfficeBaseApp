namespace OfficeBaseApp.Data.Repositories;
using OfficeBaseApp.Data.Entities;
public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> where T : class, IEntity
{
    public event EventHandler<T> ItemAdded;
    public event EventHandler<T> ItemRemoved;
    public int printPageSize { get; set; }
    public int sortBy { get; set; }
}