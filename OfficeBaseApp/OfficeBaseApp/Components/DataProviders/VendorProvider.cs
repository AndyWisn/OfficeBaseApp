using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;

namespace OfficeBaseApp.Components.DataProviders;
public class VendorProvider : GenericDataProvider<Vendor>, IVendorProvider
    {
    private readonly IRepository<Vendor> _repository;
    public VendorProvider(IRepository<Vendor> repository) : base(repository)
    {
        _repository = repository;
    }
}