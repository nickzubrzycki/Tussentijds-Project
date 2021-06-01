using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tussentijds_Project
{
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
        public User(string firstName, string lastName, string username, string password, Role role)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = Encryption(password);
            Role = role;
        }
        public User()
        {

        }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
        public static string Encryption(string password)
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
        public static char NextChar(char c)
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
    public class Order
    {
        [Key]
        public int OrderId { get; set; }        
        public Customer Customer { get; set; }        
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
    
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }        
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }        
    }
    
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }  
        public double UnitPrice { get; set; }
        public int Stock { get; set; }
        public Supplier Supplier { get; set; }
        public override string ToString()
        {
            return Name;
        }
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
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }

}
