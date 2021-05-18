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

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                cbUsers.ItemsSource = ctx.Users.ToList();
                cbRoles.ItemsSource = ctx.Roles.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = cbUsers.SelectedItem as User;
            Role selectedRole = cbRoles.SelectedItem as Role;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).FirstName = txtVoornaam.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).LastName = txtAchternaam.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Username = txtUsername.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Password = txtPassword.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Role = ctx.Roles.FirstOrDefault(r => r.RoleId == selectedRole.RoleId);
                ctx.SaveChanges();
            }          
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
