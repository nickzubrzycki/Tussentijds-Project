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
using System.Windows.Shapes;

namespace Tussentijds_Project
{
    /// <summary>
    /// Interaction logic for DataUsers.xaml
    /// </summary>
    public partial class DataUsers : Window
    {
        public DataUsers()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                dgUsers.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, UserName = u.Username, Password = u.Password, Role = r.Name }).ToList();
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
            Databeheer data = new Databeheer();
            data.Show();
        }
    }
}
