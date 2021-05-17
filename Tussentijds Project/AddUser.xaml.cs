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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();            

            using (var ctx = new OrderManagerContext())
            {
                cbRoles.ItemsSource = ctx.Roles.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Role selected = cbRoles.SelectedItem as Role;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Users.Add(new User(txtVoornaam.Text, txtAchternaam.Text, txtUsername.Text, txtPassword.Text, ctx.Roles.FirstOrDefault(r => r.RoleId == selected.RoleId)));
                ctx.SaveChanges();                    
            }
            MessageBox.Show("User werd met succes toegevoegd!");
            Close();
            DataUsers users = new DataUsers();
            users.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            DataUsers users = new DataUsers();
            users.Show();
        }
    }
}
