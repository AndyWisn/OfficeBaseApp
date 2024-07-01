using OfficeBaseApp.Components.CsvReader.Models;

namespace OfficeBaseApp.Components.CsvReader;

public interface ICsvReader
{
    List<Car> ProcessCars(string filePath);

    List<Manufacturer> ProcessManufacturers(string filePath);

}
