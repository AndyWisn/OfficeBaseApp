using OfficeBaseApp.Entities;

namespace OfficeBaseApp.DataProviders;
public interface IDataProvider<T> where T : class, IEntity
{
    public List<string> GetUniqueNames();
    public List<T> OrderByName();
    public List<T> OrderByNameDescending();
    public List<T> WhereStartsWith(string prefix);
    public List<T> SkipItems(int howMany);
    public List<T> SkipItemsWhileNameStartsWith(string prefix);
    public List<string> DistinctAllNames();
    public List<T> DistinctByNames();
    public List<T[]> ChunkItems(int size);
}