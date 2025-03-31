namespace ShopMobileWebApp.Models
{
    public partial class Order
    {
        public int SumOrder { get =>  OrderProduct == null ? 0 : OrderProduct.Select(x => x.Product.PriceRub).Sum(); }
    }
}
