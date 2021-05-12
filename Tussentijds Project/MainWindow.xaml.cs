using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CreateDB();


        }
        public void CreateDB()
        {
            using (var ctx = new OrderManagerContext())
            {
                ctx.Roles.Add(new Role() { Name = "Administrator" });
                ctx.Roles.Add(new Role() { Name = "Magazijnier" });
                ctx.Roles.Add(new Role() { Name = "Verkoper" });
                ctx.SaveChanges();

                ctx.Users.Add(new User("Nick", "Zubrzycki", "nickz", "nz1983", ctx.Roles.FirstOrDefault(r => r.RoleId == 1)));                
                ctx.SaveChanges();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new OrderManagerContext())
            {
                var user = ctx.Users.Include(nameof(Role)).FirstOrDefault(u => u.Username == txtUsername.Text);

                if (user == null)
                    MessageBox.Show("Ongeldige username");
                else
                {
                    if (user.Password == User.Encryption(txtPassword.Text))
                    {
                        ActiveUser.UserId = user.UserId;
                        ActiveUser.FirstName = user.FirstName;
                        ActiveUser.LastName = user.LastName;
                        ActiveUser.Role = user.Role.Name;
                        UserMenu menu = new UserMenu();
                        Hide();
                        menu.Show();
                    }
                    else
                        MessageBox.Show("Ongeldig password");
                }
                    
            }
        }
    }
}
