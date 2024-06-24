using Microsoft.Extensions.DependencyInjection;
using OfficeBaseApp.Entities;
namespace OfficeBaseApp.DataProviders;

public interface IComponentProviderList
{
    List<string> GetUniqueNames();

}
