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

namespace Tussentijds_Project
{
    /// <summary>
    /// Interaction logic for UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        public UserEdit()
        {
            InitializeComponent();

            using (var ctx = new OrderManagerContext())
            {
                dgUsers.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();

                cbRoles.ItemsSource = ctx.Roles.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = dgUsers.SelectedItem as User;
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
            
            
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selected = dgUsers.SelectedItem as User;

            txtVoornaam.Text = selected.FirstName;
            txtAchternaam.Text = selected.LastName;
            txtUsername.Text = selected.Username;
            txtPassword.Text = selected.Password;
            cbRoles.SelectedItem = selected.Role;
        }
    }
}
