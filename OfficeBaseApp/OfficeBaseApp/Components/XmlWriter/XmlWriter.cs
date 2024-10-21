using OfficeBaseApp.Components.DataProviders;
using System.Xml.Linq;

namespace OfficeBaseApp.Components.XmlWriter;

public class XmlWriter : IXmlWriter 
{
    private readonly IProductionPartProvider _productionPartProvider;
    private readonly IVendorProvider _vendorProvider;
    private readonly IProductProvider _productProvider;

    public XmlWriter(IProductionPartProvider productionPartProvider, IVendorProvider vendorProvider, IProductProvider productProvider)
    {
        _productionPartProvider=productionPartProvider;
        _vendorProvider = vendorProvider;
        _productProvider = productProvider;
    }

    public void CreateOutputXml()
    {
        var productionParts = _productionPartProvider.GetAllWithUniqueNames();
        var vendors = _vendorProvider.GetAllWithUniqueNames();
        var partsGroupsByVendor = vendors.GroupJoin(
            productionParts,
            vendor => vendor.Name,
            productionPart => productionPart.PartVendor,
            (m, g) => new
            {
                Vendor = m,
                ProductionParts = g
            })
            .OrderBy(x => x.Vendor.Name);

        var document = new XDocument(new XElement("Production_Parts_Grouped_by_Vendor"));
        foreach (var group in partsGroupsByVendor)
        {
            Console.WriteLine($"Parts from vendor {group.Vendor.Name}:");         
            var element = new XElement("Vendor", new XAttribute("Vendor_Name", group.Vendor.Name));
            foreach (var productionPart in group.ProductionParts.OrderBy(x => x.Price))
            {
                Console.WriteLine($"\t{productionPart.Name} Price:{productionPart.Price}");
                element.Add(new XElement("ProductionPart", new XAttribute("Part_Name", productionPart.Name),
                                                           new XAttribute("Part_Price",productionPart.Price)));
            }
            document.Root.Add(element);
        }
        document.Save("ProductionPartsOutput.xml");
    }
}