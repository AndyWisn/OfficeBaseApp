namespace OfficeBaseApp.Entities
{
    public class Product : TradeGoodsBase
    {
        public Product()
        {
        }
        public Product(string name, string description)
        {
            this.Name = name;
            this.Description = description;

        }
        public List<Component> productionComponents = new();
        public string ComponentListInString
        {
            get
            {
                string stringList = null;
                if (this.productionComponents.Count>0)
                {
                    for (int i = 0; i < this.productionComponents.Count - 1; i++)
                    {
                        stringList += this.productionComponents[i].Name + ",";
                    }
                    stringList += this.productionComponents[this.productionComponents.Count - 1].Name + ".";
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
                foreach (Component component in this.productionComponents)
                {
                    productComponentCostSum += component.Price;
                }
                return productComponentCostSum;
            }
            set { }
        }
        public void AddProductionComponent(Component part)
        {
            this.productionComponents.Add(part);
        }
        public void RemoveProductionComponent(Component part)
        {
            this.productionComponents.Remove(part);
        }
        public override string ToString() => string.Format("{0,-13} {1,30} {2,12} {3,10}", "Product", base.ToString(), "|Components:", this.ComponentListInString);
    }
}