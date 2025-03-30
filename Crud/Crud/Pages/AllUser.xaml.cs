using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AllUser.xaml
    /// </summary>
    public partial class AllUser : Page
    {
        public AllUser()
        {
            InitializeComponent();
            Refresh();
        }

        private void Refresh()
        {
            DGUser.ItemsSource = App.Db.User.ToList();
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser(new User()));
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
if(DGUser.SelectedItem is User user)
            {
                NavigationService.Navigate(new AddUser(user));

            }
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            if (DGUser.SelectedItem is User user)
            {
               App.Db.User.Remove(user);
                App.Db.SaveChanges();
                Refresh();

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();

        }
    }
}
