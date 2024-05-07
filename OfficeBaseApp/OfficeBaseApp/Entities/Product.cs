namespace OfficeBaseApp.Entities
{
    public class Product : TradeGoodsBase
    {
        public Product()
        {
        }
        
        public Product(string name)
        { 
        }

        private List<Component> components  = new();
        
    }
}
