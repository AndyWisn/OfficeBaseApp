using Microsoft.IdentityModel.Tokens;

namespace OfficeBaseApp.Data.Entities;
public class Product : TradeGoodsBase
{
    public Product()
    {
    }
    public Product(string name, string description, List<int> productionComponentList)
    {
        Name = name;
        Description = description;
        ProductionComponentsId = productionComponentList;
    }
    public List<int> ProductionComponentsId { get; set; } = new List<int>();
    public string ComponentListInString
    {
        get
        {
            string stringList = "";
            if (ProductionComponentsId.Count > 0)
            {
                for (int i = 0; i < ProductionComponentsId.Count - 1; i++)
                {
                    stringList += ProductionComponentsId[i].ToString() + ",";
                }
                stringList += ProductionComponentsId[ProductionComponentsId.Count - 1].ToString() + ".";
            }
            return stringList;
        }
        set { }
    }
    public float ProductCost
    {
        get
        {
            float productComponentCostSum = 0;
            foreach (var componentId in ProductionComponentsId)
            {
                //productComponentCostSum += component.Price;
            }
            return productComponentCostSum;
        }
        set { }
    }
    public override void EnterPropertiesFromConsole()
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
        ProductionComponentsId = componentList;
        Console.CursorVisible = false;
        //return new Product(name, description, componentList);
    }
    public override string ToString() => string.Format("{0,-13} {1,30} {2,12} {3,10}", "Product", base.ToString(), "|Components:", ComponentListInString);
}