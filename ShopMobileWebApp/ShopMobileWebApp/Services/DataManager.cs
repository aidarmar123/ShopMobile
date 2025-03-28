using ShopMobileWebApp.Models;

namespace ShopMobileWebApp.Services
{
    public static class DataManager
    {
        public static List<Product> Products;
        public static List<Order> Orders;
        public static List<OrderProduct> OrderProducts;
        public static List<Category> Categorys;
        public static List<PhotoProduct> PhotoProducts;
        public static List<Role> Roles;
        public static List<User> Users;

        public static async Task InitData()
        {
            Roles = await NetManager.Get<List<Role>>("api/Roles");
            Users = await NetManager.Get<List<User>>("api/Users");
            Categorys = await NetManager.Get<List<Category>>("api/Categories");
            Orders = await NetManager.Get<List<Order>>("api/Orders");
            PhotoProducts = await NetManager.Get<List<PhotoProduct>>("api/PhotoProducts");
            Products = await NetManager.Get<List<Product>>("api/Products");
            OrderProducts = await NetManager.Get<List<OrderProduct>>("api/OrderProducts");
        }
    }
}
