namespace OfficeBaseApp.Entities
{
    public class Product : TradeGoodsBase
    {
        public Product()
        {
        }

        public Product(string name, Vendor? vendor, float price)
        {
            this.Name = name;
            this.Vendor = vendor;
            this.Price = price;
        }
        public Vendor? Vendor { get; set; }
        public float Price
        {
            get
            {
                float price = 0;
                foreach (var partSet in productionComponents)
                {
                    price += partSet.quantity * partSet.component.Price;
                }
                return price;
            }
            set { }
        }

        public struct PartSet
        {
            public Component component;
            public int quantity;
        }

        public List<PartSet> productionComponents = new();

        public void AddProductionComponent(Component part, int qty)
        {
            var partSetToAdd = new PartSet { component = part, quantity = qty };
            productionComponents.Add(partSetToAdd);
        }

        public void RemoveProductionComponent(Component part, int qty)
        {
            var partSetToRemove = new PartSet { component = part, quantity = qty };
            foreach(var removeComp in productionComponents)
            { f (removeComp = partSetToRemove) productionComponents.Remove(removeComp); }           //dopisać!!
            
        }

        public override string ToString() => "Product " + base.ToString();


    }
}
