using Microsoft.IdentityModel.Tokens;

namespace OfficeBaseApp.Data.Entities;
public class Product : IEntity
{
    public Product()
    {
    }
    public Product(string name, string description, List<int> productionComponentList)
    {
        Name = name;
        Description = description;
        ProductionPartsId = productionComponentList;
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<int> ProductionPartsId { get; set; } = new List<int>();
    public float Price { get; set; }
    public string? ComponentListInString
    {
        get
        {
            string stringList = "";
            if (ProductionPartsId.Count > 0)
            {
                for (int i = 0; i < ProductionPartsId.Count - 1; i++)
                {
                    stringList += ProductionPartsId[i].ToString() + ",";
                }
                stringList += ProductionPartsId[ProductionPartsId.Count - 1].ToString() + ".";
            }
            return stringList;
        }
        set { }
    }
    public void EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Product to repository:");
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        Name = Console.ReadLine();
        Console.WriteLine("Enter descryption:");
        Description = Console.ReadLine();
        Console.WriteLine("Enter components IDs like 1,2,3,4:");
        string? components = Console.ReadLine();
        List<int> componentList = null;
        if (!components.IsNullOrEmpty()) { componentList = components.Split(',').Select(int.Parse).ToList(); }
        ProductionPartsId = componentList;
        Console.CursorVisible = false;
        //return new Product(name, description, componentList);
    }
    public override string ToString() => string.Format("{0,-5} {1,-35} {2,-30} {3,-5} {4,-5}", Id, Name, Description, Price, ComponentListInString);
}