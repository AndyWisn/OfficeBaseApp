using Microsoft.IdentityModel.Tokens;
using OfficeBaseApp.Data.Entities;
using OfficeBaseApp.Data.Repositories;
using OfficeBaseApp.Components.DataProviders;
using OfficeBaseApp.Components;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

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


        var productionParts = _productionPartProvider.GetAll();
        var vendors = _vendorProvider.GetAll();
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

        var document = new XDocument(new XElement("Production_Parts_By_Vendor"));
        foreach (var group in partsGroupsByVendor)
        {
            Console.WriteLine($"Parts from vendor {group.Vendor.Name}:");
            var element = new XElement("Vendor", new XAttribute("Vendor_Name",group.Vendor.Name));
            foreach (var productionPart in group.ProductionParts)
            {
                Console.WriteLine($"\t{productionPart.Name}");
                element.Add(new XElement("ProductionPart", new XAttribute("Part_Name", productionPart.Name)));
            }
            document.Root.Add(element);
        }
        document.Save("ProductionPartsOutput.xml");





        //foreach (var group in groups)
        //{
        //    var element = new XElement("Manufacturer",
        //                        new XAttribute("Name", group.Manufacturer.Name),
        //                        new XAttribute("Country", group.Manufacturer.Country),
        //                        new XElement("Cars",
        //                        new XAttribute("Country", group.Manufacturer.Country),
        //                        new XAttribute("CombinedSum", group.Cars.Sum(x => x.Combined)),
        //                        group.Cars.OrderByDescending(x => x.Combined)
        //                                   .Select(x => new XElement("Car",
        //                                               new XAttribute("Model", x.Name),
        //                                               new XAttribute("Combined", x.Combined)))
        //                        ));
        //    document.Root.Add(element);
        //}
        //document.Save("Output.xml");
    }

    //public void QueryXml()
    //{
    //    var document = XDocument.Load("fuel.xml");
    //    var names = document
    //        .Element("Cars")?
    //        .Elements("Car")
    //        .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
    //        .Select(x => x.Attribute("Name")?.Value);
    //    foreach (var name in names)
    //    { Console.WriteLine(name); }
    //}

    //public void CreateXml()
    //{
    //    var records = _csvReader.ProcessCars("Resources\\Files\\fuel.csv");
    //    var document = new XDocument();
    //    var cars = new XElement("Cars", records
    //                                     .Select(x =>
    //                                     new XElement("Car",
    //                                     new XAttribute("Name", x.Name),
    //                                     new XAttribute("Combined", x.Combined),
    //                                     new XAttribute("Manufacturer", x.Manufacturer))
    //                                            )
    //                            );

    //    document.Add(cars);
    //    document.Save("fuel.xml");
    //}


}
