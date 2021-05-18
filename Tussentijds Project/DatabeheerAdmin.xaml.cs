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
                dgUsers.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();

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

                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = ctx.Users.Join(ctx.Roles,
                        u => u.Role.RoleId,
                        r => r.RoleId,
                        (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
            }
            txtVoornaam.Clear();
            txtAchternaam.Clear();
            txtUsername.Clear();
            txtPassword.Clear();           
            
        }
    }
}
