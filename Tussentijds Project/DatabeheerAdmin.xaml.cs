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
    /// Interaction logic for DatabeheerAdmin.xaml
    /// </summary>
    public partial class DatabeheerAdmin : Window
    {
        public DatabeheerAdmin()
        {
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";
            

            using (var ctx = new OrderManagerContext())
            {
                dgUsersAdd.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();

                cbRolesAdd.ItemsSource = ctx.Roles.ToList();

                dgUsersEdit.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();

                cbUsersEdit.ItemsSource = ctx.Users.ToList();
                cbRolesEdit.ItemsSource = ctx.Roles.ToList();
            }            
        }    
                
        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            Role selected = cbRolesAdd.SelectedItem as Role;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Users.Add(new User(txtVoornaamAdd.Text, txtAchternaamAdd.Text, txtUsernameAdd.Text, txtPasswordAdd.Text, ctx.Roles.FirstOrDefault(r => r.RoleId == selected.RoleId)));
                ctx.SaveChanges();

                dgUsersAdd.ItemsSource = null;
                dgUsersAdd.ItemsSource = ctx.Users.Join(ctx.Roles,
                        u => u.Role.RoleId,
                        r => r.RoleId,
                        (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();

                dgUsersEdit.ItemsSource = null;
                dgUsersEdit.ItemsSource = ctx.Users.Join(ctx.Roles,
                   u => u.Role.RoleId,
                   r => r.RoleId,
                   (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
                dgUsersEdit.SelectedItem = ctx.Users;
            }            
            txtVoornaamAdd.Clear();
            txtAchternaamAdd.Clear();
            txtUsernameAdd.Clear();
            txtPasswordAdd.Clear();
            cbRolesAdd.SelectedItem = null;
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            User selectedUser = dgUsersEdit.SelectedItem as User;
            Role selectedRole = cbRolesEdit.SelectedItem as Role;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).FirstName = txtVoornaamEdit.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).LastName = txtAchternaamEdit.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Username = txtUsernameEdit.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Password = txtPasswordEdit.Text;
                ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Role = ctx.Roles.FirstOrDefault(r => r.RoleId == selectedRole.RoleId);
                ctx.SaveChanges();

                dgUsersAdd.ItemsSource = null;
                dgUsersAdd.ItemsSource = ctx.Users.Join(ctx.Roles,
                        u => u.Role.RoleId,
                        r => r.RoleId,
                        (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();

                dgUsersEdit.ItemsSource = null;
                dgUsersEdit.ItemsSource = ctx.Users.Join(ctx.Roles,
                   u => u.Role.RoleId,
                   r => r.RoleId,
                   (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
            }
            cbUsersEdit.SelectedItem = null;
            txtVoornaamEdit.Clear();
            txtAchternaamEdit.Clear();
            txtUsernameEdit.Clear();
            txtPasswordEdit.Clear();
            cbRolesEdit.SelectedItem = null;

        }        

        private void cbUsersEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selected = cbUsersEdit.SelectedItem as User;

            txtVoornaamEdit.Text = selected.FirstName;
            txtAchternaamEdit.Text = selected.LastName;
            txtUsernameEdit.Text = selected.Username;
            txtPasswordEdit.Text = selected.Password;
            cbRolesEdit.SelectedItem = selected.Role;
        }
    }
}
