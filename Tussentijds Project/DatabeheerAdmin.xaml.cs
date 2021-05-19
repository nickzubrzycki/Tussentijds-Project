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

                dgUsersDelete.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
                cbUsersDelete.ItemsSource = ctx.Users.ToList();
            }
        }

        private void Button_Click_AddUser(object sender, RoutedEventArgs e)
        {
            if (txtVoornaamAdd.Text != null && txtAchternaamAdd.Text != null && txtUsernameAdd.Text != null && txtPasswordAdd.Text != null && cbRolesAdd.SelectedItem != null)
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
                    cbUsersEdit.ItemsSource = ctx.Users.ToList();

                    dgUsersDelete.ItemsSource = null;
                    dgUsersDelete.ItemsSource = ctx.Users.Join(ctx.Roles,
                        u => u.Role.RoleId,
                        r => r.RoleId,
                        (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
                    cbUsersDelete.ItemsSource = ctx.Users.ToList();

                }
                MessageBox.Show("User werd toegevoegd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtVoornaamAdd.Clear();
                txtAchternaamAdd.Clear();
                txtUsernameAdd.Clear();
                txtPasswordAdd.Clear();
                cbRolesAdd.SelectedItem = null;
            }
            else
                MessageBox.Show("User toevoegen mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);            
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (cbUsersEdit.SelectedItem != null)
            {
                User selectedUser = cbUsersEdit.SelectedItem as User;
                Role selectedRole = cbRolesEdit.SelectedItem as Role;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).FirstName = txtVoornaamEdit.Text;
                    ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).LastName = txtAchternaamEdit.Text;
                    ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Username = txtUsernameEdit.Text;
                    ctx.Users.FirstOrDefault(u => u.UserId == selectedUser.UserId).Password = Encryption(txtPasswordEdit.Text);
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
                    cbUsersEdit.ItemsSource = ctx.Users.ToList();                    

                    dgUsersDelete.ItemsSource = null;
                    dgUsersDelete.ItemsSource = ctx.Users.Join(ctx.Roles,
                        u => u.Role.RoleId,
                        r => r.RoleId,
                        (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
                    cbUsersDelete.ItemsSource = ctx.Users.ToList();
                }
                MessageBox.Show("De wijzigingen werden opgeslagen.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een user te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);            
        }

        private void cbUsersEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selected = cbUsersEdit.SelectedItem as User;

            txtVoornaamEdit.Text = selected.FirstName;
            txtAchternaamEdit.Text = selected.LastName;
            txtUsernameEdit.Text = selected.Username;
            txtPasswordEdit.Text = Decryption(selected.Password);
            cbRolesEdit.SelectedItem = selected.Role;
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (cbUsersDelete.SelectedItem != null)
            {
                User selected = cbUsersDelete.SelectedItem as User;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Users.Remove(ctx.Users.FirstOrDefault(u => u.UserId == selected.UserId));
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
                    cbUsersEdit.ItemsSource = ctx.Users.ToList();

                    dgUsersDelete.ItemsSource = null;
                    dgUsersDelete.ItemsSource = ctx.Users.Join(ctx.Roles,
                        u => u.Role.RoleId,
                        r => r.RoleId,
                        (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
                    cbUsersDelete.ItemsSource = ctx.Users.ToList();
                }
                MessageBox.Show("De user werd verwijderd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een user te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static string Encryption(string password)
        {
            char key = (char)1;
            char[] charPassword = password.ToCharArray();

            for (int i = 0; i < charPassword.Length; i++)
            {
                if (i == 127)
                {
                    charPassword[i] = (char)1;
                }
                else
                {
                    charPassword[i] = (char)(charPassword[i] + key);
                }
            }
            string encryptedPassword = new string(charPassword);
            return encryptedPassword;
        }

        public static string Decryption(string password)
        {
            char key = (char)1;
            char[] charPassword = password.ToCharArray();

            for (int i = 0; i < charPassword.Length; i++)
            {
                if (i == 127)
                {
                    charPassword[i] = (char)1;
                }
                else
                {
                    charPassword[i] = (char)(charPassword[i] - key);
                }
            }
            string encryptedPassword = new string(charPassword);
            return encryptedPassword;
        }        

        
    }
}
