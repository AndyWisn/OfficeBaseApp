using OfficeBaseApp.Components.CsvReader.Extensions;
using OfficeBaseApp.Components.CsvReader.Models;
using System.Diagnostics.Metrics;
using System.Globalization;

namespace OfficeBaseApp.Components.CsvReader;

public class CsvReader : ICsvReader
{
    public List<ProductModel> ProcessProducts(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No such a filename at this path.");
            return new List<ProductModel>();            
        }
        else
        {
            var products = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new ProductModel()
                    {
                        Name = columns[0],
                        Price = float.Parse(columns[1], CultureInfo.InvariantCulture),
                        Description = columns[2],
                    };
                });
            return products.ToList();
        }
    }

    public List<ProductionPartModel> ProcessProductionParts(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No such a filename at this path.");
            return new List<ProductionPartModel>();
        }
        else
        {
            var productionParts = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new ProductionPartModel()
                    {
                        Name = columns[0],
                        PartManufacturer = columns[1],
                        Description = columns[2],
                        Price = float.Parse(columns[3], CultureInfo.InvariantCulture),
                        PartVendor = columns[4],
                    };
                });
            return productionParts.ToList();
        }
    }
    public List<VendorModel> ProcessVendors(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No such a filename at this path.");
            return new List<VendorModel>();
        }
        else
        {
            var vendors = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new VendorModel()
                    {
                        Name = columns[0],
                        Contact = columns[1],
                        Description = columns[2],
                        Country = columns[3],
                        VendorCertificate = columns[4],
                    };
                });
            return vendors.ToList();
        }
    }
}