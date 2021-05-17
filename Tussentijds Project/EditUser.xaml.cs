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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public EditUser()
        {
            InitializeComponent();

            using (var ctx = new OrderManagerContext())
            {
                cbUsers.ItemsSource = ctx.Users.ToList();
                cbRoles.ItemsSource = ctx.Roles.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User selected = cbUsers.SelectedItem as User;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Users.FirstOrDefault(u => u.FirstName == selected.FirstName).FirstName = txtVoornaam.Text;
                ctx.Users.FirstOrDefault(u => u.LastName == selected.LastName).LastName = txtAchternaam.Text;
                ctx.Users.FirstOrDefault(u => u.Username == selected.Username).Username = txtUsername.Text;
                ctx.Users.FirstOrDefault(u => u.Password == selected.Password).Password = txtPassword.Text;
                ctx.Users.FirstOrDefault(u => u.Role == selected.Role).Role = (Role)cbRoles.SelectedItem;
                ctx.SaveChanges();
            }          
            Close();
            DataUsers users = new DataUsers();
            users.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selected = cbUsers.SelectedItem as User;

            txtVoornaam.Text = selected.FirstName;
            txtAchternaam.Text = selected.LastName;
            txtUsername.Text = selected.Username;
            txtPassword.Text = selected.Password;
            cbRoles.SelectedItem = selected.Role;
        }
    }
}
