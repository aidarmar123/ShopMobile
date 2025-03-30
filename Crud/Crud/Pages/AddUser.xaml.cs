using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Crud.Models;

namespace Crud.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        User contextUser;
        public AddUser(Models.User user)
        {
            InitializeComponent();
            contextUser = user;
            CbRole.ItemsSource = App.Db.Role.ToList();
            DataContext = contextUser;
        }

        private void BSAve_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder er = ValidationData(contextUser);
            if (er.Length > 0)
            {
                MessageBox.Show(er.ToString());
            }
            else
            {
                if (contextUser.Id == 0)
                    App.Db.User.Add(contextUser);
                App.Db.SaveChanges();
                NavigationService.GoBack();
            }
        }
         /// <summary>
        /// Метод для валидации модели
        /// </summary>
        /// <param name="data">Модели для проверки</param>
        /// <returns>Возращает ошибки</returns>
        private StringBuilder ValidationData<T>(T data)
        {
            var error = new StringBuilder();
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data);
            var validationResult = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (!Validator.TryValidateObject(data, validationContext, validationResult, true))
            {
                foreach (var item in validationResult)
                {
                    error.AppendLine(item.ToString());
                }
            }
            return error;
        }
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }
    }
}
