using OfficeBaseApp.Components.CsvReader.Models;

namespace OfficeBaseApp.Components.CsvReader;

public interface ICsvReader
{
    List<ProductModel> ProcessProducts(string filePath);
    List<ProductionPartModel> ProcessProductionParts(string filePath);
    List<VendorModel> ProcessVendors(string filePath);


}
