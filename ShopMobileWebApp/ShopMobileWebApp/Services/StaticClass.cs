using System.Threading.Tasks;
using ShopMobileWebApp.Models;

namespace ShopMobileWebApp.Services
{
    public static class StaticClass
    {
        private static User userAuth;

        public static User UserAuth
        {
            get { return userAuth; }

        }

        public static void SetUserAuth(string login, string password)
        {
            userAuth = DataManager.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
        }

        public static async Task NewUser(User user)
        {
            userAuth = await NetManager.Post("api/Users", user);
        }
    }
}
