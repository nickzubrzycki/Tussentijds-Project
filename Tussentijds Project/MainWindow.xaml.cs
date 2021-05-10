using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            using (var ctx = new OrderManagerContext())
            {
                ctx.Roles.Add(new Role() { Name = "Administrator" });
                ctx.Roles.Add(new Role() { Name = "Magazijnier" });
                ctx.Roles.Add(new Role() { Name = "Verkoper" });
                ctx.Users.Add(new User()
                {
                    FirstName = "Nick",
                    LastName = "Zubrzycki",
                    Username = "nzubrzycki",
                    Password = "nz17983",
                    
                });
            }
        }
        static void CreateDB()
        {

        }
        public class User
        {
            [Key]
            public int UserId { get; set; }
            [MaxLength(50)]
            public string FirstName { get; set; }
            [MaxLength(50)]
            public string LastName { get; set; }
            [MaxLength(50)]
            public string Username { get; set; }
            [MaxLength(50)]
            public string Password { get; set; }
            public Role Role { get; set; }

            public string EncryptPassword()
            {
                char[] charPassword = Password.ToCharArray();

                for (int i = 0; i < charPassword.Length; i++)
                {

                    if (i == 127)
                    {
                        charPassword[i] = (char)1;
                    }
                    else
                    {
                        charPassword[i] = NextChar(charPassword[i]);
                    }
                }
                string encryptedPassword = new string(charPassword);
                return encryptedPassword;
            }
            static char NextChar(char c)
            {
                int ascii = c;
                int nextascii = ascii + 1;
                char nextChar = (char)nextascii;
                return nextChar;
            }
        }
        public class Role
        {
            [Key]
            public int RoleId { get; set; }
            public string Name { get; set; }
        }
        public class OrderManagerContext : DbContext
        {
            public OrderManagerContext() : base("name=OrderManagerDBConnectString")
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderManagerContext>());
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
        }
    }
}
