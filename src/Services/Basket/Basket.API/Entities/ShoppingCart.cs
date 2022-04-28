namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }

        public ShoppingCart(string username)
        {
            UserName = username;
        }

        public string UserName { get; set; } = string.Empty;
        public List<ShoppingCartItem> Items { get; set; } = new();

        //public decimal TotalPrice 
        //{
        //    get 
        //    {
        //        return Items.Sum(x => x.Price * x.Quantity);
        //    } 
        //}

        public decimal TotalPrice { get => Items.Sum(x => x.Quantity * x.Price); }

        //{
        //    return Items.Sum(x => x.Price);
        //}
    }
}
