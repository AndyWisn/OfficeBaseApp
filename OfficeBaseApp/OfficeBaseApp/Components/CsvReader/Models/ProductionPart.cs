namespace OfficeBaseApp.Components.CsvReader.Models;

public class ProductionPartModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PartManufacturer { get; set; }
    public string? Description { get; set; }
    public string? PartVendor { get; set; }
    public float Price { get; set; }
}