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
            CreateDB();


        }
        public void CreateDB()
        {
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
                    RoleId = 1
                });
                ctx.SaveChanges();
            }
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
            private string password;
            public string Password
            {
                get { return password; }
                set { password = EncryptPassword(value); }
            }
            public int RoleId { get; set; }
            public Role Role { get; set; }

            public string EncryptPassword(string password)
            {
                char[] charPassword = password.ToCharArray();

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
            [MaxLength(50)]
            public string Name { get; set; }
        }
        public class CustomerOrder
        {
            [Key]
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }
            public DateTime OrderDate { get; set; }
           
        }
        public class SupplyOrder
        {
            [Key]
            public int OrderId { get; set; }
            public int SupplierId { get; set; }
            public Supplier Supplier { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }
            public DateTime OrderDate { get; set; }
            
        }
        public class CustomerOrderDetail
        {
            [Key]
            [Column(Order = 1)]
            public int OrderId { get; set; }
            public CustomerOrder CustomerOrder { get; set; }
            [Key]
            [Column(Order = 2)]
            public int ProductId { get; set; }
            public double UnitPrice { get; set; }
            public int Quantity { get; set; }

        }
        public class SupplyOrderDetail
        {
            [Key]
            [Column(Order = 1)]
            public int OrderId { get; set; }
            public SupplyOrder SupplyOrder { get; set; }
            [Key]
            [Column(Order = 2)]
            public int ProductId { get; set; }
            public double UnitPrice { get; set; }
            public int Quantity { get; set; }

        }
        public class Product
        {
            [Key]
            public int ProductId { get; set; }
            [MaxLength(100)]
            public string Name { get; set; }
            [MaxLength(50)]
            public string Description { get; set; }
            public int Stock { get; set; }
            public double UnitPrice { get; set; }
            public int SupplierId { get; set; }
            public Supplier Supplier { get; set; }
            
        }
        public class Customer
        {
            [Key]
            public int CustomerId { get; set; }
            [MaxLength(50)]
            public string Name { get; set; }
            [MaxLength(50)]
            public string Address { get; set; }
        }
        public class Supplier
        {
            [Key]
            public int SupplierId { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
        }
        public class OrderManagerContext : DbContext
        {
            public OrderManagerContext() : base("name=OrderManagerDBConnectString")
            {
                Database.SetInitializer(new DropCreateDatabaseIfModelChanges<OrderManagerContext>());
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<CustomerOrder> CustomerOrders { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Customer> Customers { get; set; }
        }
    }
}
