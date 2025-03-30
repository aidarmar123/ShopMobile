using System.Threading.Tasks;
using ShopMobileWebApp.Models;

namespace ShopMobileWebApp.Services
{
    public static class StaticClass
    {


        private static Order orderContext;

        public static Order OrderContext
        {
            get { return orderContext; }
            set { orderContext = value; }
        }



        private static User userAuth;

        public static User UserAuth
        {
            get { return userAuth; }

        }

        public static void SetUserAuth(string login, string password)
        {
            userAuth = DataManager.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (userAuth != null)
            {
                var orderProduct = DataManager.OrderProducts.FirstOrDefault(x => x.Order.UserId == userAuth.Id && !x.Order.IsFinish);
                orderContext = orderProduct != null ? orderProduct.Order : null;
            }
        }

        public static async Task NewUser(User user)
        {
            userAuth = await NetManager.Post("api/Users", user);
        }
    }
}
