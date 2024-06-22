using Microsoft.IdentityModel.Tokens;

namespace OfficeBaseApp.Entities;
    public class Product : TradeGoodsBase
{
    public Product()
    {
    }
    public Product(string name, string description, List<int>productionComponentList)
    {
        this.Name = name;
        this.Description = description;
        this.ProductionComponentsId = productionComponentList;

    }
    public List<int> ProductionComponentsId { get; set; } = new List<int>();
    public string ComponentListInString
    {
        get
        {
            string stringList = "";
            if (this.ProductionComponentsId.Count > 0)
            {
                for (int i = 0; i < this.ProductionComponentsId.Count - 1; i++)
                {
                    stringList += this.ProductionComponentsId[i].ToString() + ",";
                }
                stringList += this.ProductionComponentsId[this.ProductionComponentsId.Count - 1].ToString() + ".";
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
            foreach (var componentId in this.ProductionComponentsId)
            {
                //productComponentCostSum += component.Price;
            }
            return productComponentCostSum;
        }
        set { }
    }
    public Product EnterPropertiesFromConsole()
    {
        Console.WriteLine();
        Console.WriteLine($"Add new Product to repository:");
        Console.WriteLine();
        Console.CursorVisible = true;
        Console.WriteLine("Enter name:");
        string? name = Console.ReadLine();
        Console.WriteLine("Enter descryption:");
        string? description = Console.ReadLine();
        Console.WriteLine("Enter components IDs like 1,2,3,4:");
        string? components = Console.ReadLine();
        List<int> componentList=null;
        if (!components.IsNullOrEmpty()) { componentList = components.Split(',').Select(int.Parse).ToList(); }
        
        Console.CursorVisible = false;
        return new Product(name, description, componentList);
    }
    public override string ToString() => string.Format("{0,-13} {1,30} {2,12} {3,10}", "Product", base.ToString(), "|Components:", this.ComponentListInString);
}