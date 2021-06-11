using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
                cbUsersEdit.ItemsSource = ctx.Users.Include(u => u.Role).ToList();
                cbRolesEdit.ItemsSource = ctx.Roles.ToList();

                dgUsersDelete.ItemsSource = ctx.Users.Join(ctx.Roles,
                    u => u.Role.RoleId,
                    r => r.RoleId,
                    (u, r) => new { FirstName = u.FirstName, LastName = u.LastName, Username = u.Username, Password = u.Password, Role = r.Name }).ToList();
                cbUsersDelete.ItemsSource = ctx.Users.ToList();

                dgProductsAdd.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                cbSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                dgProductsEdit.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                cbProductsEdit.ItemsSource = ctx.Products.Include(p => p.Supplier).ToList();
                cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();

                dgProductsDelete.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                cbProductsDelete.ItemsSource = ctx.Products.ToList();

                dgSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                dgSuppliersEdit.ItemsSource = ctx.Suppliers.ToList();
                cbSuppliersSupplierEdit.ItemsSource = ctx.Suppliers.ToList();

                dgSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();
                cbSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();

                dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                cbCustomersDelete.ItemsSource = ctx.Customers.ToList();
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

        private void Button_Click_EditUser(object sender, RoutedEventArgs e)
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
                txtVoornaamEdit.Clear();
                txtAchternaamEdit.Clear();
                txtUsernameEdit.Clear();
                txtPasswordEdit.Clear();
                cbRolesEdit.SelectedItem = null;
            }
            else
                MessageBox.Show("Gelieve eerst een user te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);            
        }

        private void cbUsersEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selected = cbUsersEdit.SelectedItem as User;

            if (selected != null)
            {
                txtVoornaamEdit.Text = selected.FirstName;
                txtAchternaamEdit.Text = selected.LastName;
                txtUsernameEdit.Text = selected.Username;
                txtPasswordEdit.Text = Decryption(selected.Password);
                cbRolesEdit.SelectedItem = selected.Role;
            }            
        }

        private void Button_Click_DeleteUser(object sender, RoutedEventArgs e)
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

        private void Button_Click_AddProduct(object sender, RoutedEventArgs e)
        {
            if (txtNaamProductAdd.Text != null && txtPrijsAdd.Text != null && txtAantalAdd.Text != null && cbSuppliersAdd.SelectedItem != null)
            {
                Supplier selected = cbSuppliersAdd.SelectedItem as Supplier;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Products.Add(new Product()
                    { Name = txtNaamProductAdd.Text, UnitPrice = Convert.ToDouble(txtPrijsAdd.Text), Stock = Convert.ToInt32(txtAantalAdd.Text), Supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId) });
                    ctx.SaveChanges();

                    dgProductsAdd.ItemsSource = null;
                    dgProductsAdd.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbProductsEdit.ItemsSource = ctx.Products.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbProductsDelete.ItemsSource = ctx.Products.ToList();
                }
                MessageBox.Show("Product werd toegevoegd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamProductAdd.Clear();
                txtPrijsAdd.Clear();
                txtAantalAdd.Clear();               
                cbSuppliersAdd.SelectedItem = null;
            }
            else
                MessageBox.Show("Product toevoegen mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_EditProduct(object sender, RoutedEventArgs e)
        {
            if (cbProductsEdit.SelectedItem != null)
            {
                Product selectedProduct = cbProductsEdit.SelectedItem as Product;
                Supplier selectedSupplier = cbSuppliersProductEdit.SelectedItem as Supplier;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).Name = txtNaamProductEdit.Text;
                    ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).UnitPrice = Convert.ToDouble(txtPrijsEdit.Text);
                    ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).Stock = Convert.ToInt32(txtAantalEdit.Text);
                    ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).Supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selectedSupplier.SupplierId);
                    ctx.SaveChanges();

                    dgProductsAdd.ItemsSource = null;
                    dgProductsAdd.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbProductsEdit.ItemsSource = ctx.Products.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbProductsDelete.ItemsSource = ctx.Products.ToList();
                }
                MessageBox.Show("De wijzigingen werden opgeslagen.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamProductEdit.Clear();
                txtPrijsEdit.Clear();
                txtAantalEdit.Clear();                
                cbSuppliersProductEdit.SelectedItem = null;
            }
            else
                MessageBox.Show("Gelieve eerst een product te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbProductsEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selected = cbProductsEdit.SelectedItem as Product;

            if (selected != null)
            {
                txtNaamProductEdit.Text = selected.Name;
                txtPrijsEdit.Text = selected.UnitPrice.ToString();
                txtAantalEdit.Text = selected.Stock.ToString();                
                cbSuppliersProductEdit.SelectedItem = selected.Supplier;
            }
        }

        private void Button_Click_DeleteProduct(object sender, RoutedEventArgs e)
        {
            if (cbProductsDelete.SelectedItem != null)
            {
                Product selected = cbProductsDelete.SelectedItem as Product;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.OrderDetails.RemoveRange(ctx.OrderDetails.Where(od => od.Product.ProductId == selected.ProductId));
                    ctx.Products.Remove(ctx.Products.FirstOrDefault(p => p.ProductId == selected.ProductId));
                    ctx.SaveChanges();

                    dgProductsAdd.ItemsSource = null;
                    dgProductsAdd.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbProductsEdit.ItemsSource = ctx.Products.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbProductsDelete.ItemsSource = ctx.Products.ToList();
                }
                MessageBox.Show("Het product werd verwijderd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een product te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_AddSupplier(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNaamSupplierAdd.Text) && !string.IsNullOrWhiteSpace(txtAdresSupplierAdd.Text))
            {
                using (var ctx = new OrderManagerContext())
                {
                    ctx.Suppliers.Add(new Supplier() { Name = txtNaamSupplierAdd.Text, Address = txtAdresSupplierAdd.Text });
                    ctx.SaveChanges();

                    dgSuppliersAdd.ItemsSource = null;
                    dgSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                    dgSuppliersEdit.ItemsSource = null;
                    dgSuppliersEdit.ItemsSource = ctx.Suppliers.ToList();                    

                    dgSuppliersDelete.ItemsSource = null;
                    dgSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();

                    cbSuppliersSupplierEdit.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();
                }
                MessageBox.Show("Leverancier werd toegevoegd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamSupplierAdd.Clear();
                txtAdresSupplierAdd.Clear();                
            }
            else
                MessageBox.Show("Leverancier toevoegen mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);        
        }

        private void Button_Click_EditSupplier(object sender, RoutedEventArgs e)
        {
            if (cbSuppliersSupplierEdit.SelectedItem != null)
            {
                Supplier selected = cbSuppliersSupplierEdit.SelectedItem as Supplier;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId).Name = txtNaamSupplierEdit.Text;
                    ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId).Address = txtAdresSupplierEdit.Text;
                    ctx.SaveChanges();

                    dgSuppliersAdd.ItemsSource = null;
                    dgSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                    dgSuppliersEdit.ItemsSource = null;
                    dgSuppliersEdit.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersSupplierEdit.ItemsSource = ctx.Suppliers.ToList();

                    dgSuppliersDelete.ItemsSource = null;
                    dgSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();

                    dgProductsAdd.ItemsSource = null;
                    dgProductsAdd.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                }
                MessageBox.Show("De wijzigingen werden opgeslagen.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamSupplierEdit.Clear();
                txtAdresSupplierEdit.Clear();
            }
            else
                MessageBox.Show("Gelieve eerst een leverancier te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbSuppliersSupplierEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Supplier selected = cbSuppliersSupplierEdit.SelectedItem as Supplier;

            if (selected != null)
            {
                txtNaamSupplierEdit.Text = selected.Name;
                txtAdresSupplierEdit.Text = selected.Address;                
            }
        }

        private void Button_Click_DeleteSupplier(object sender, RoutedEventArgs e)
        {
            if (cbSuppliersDelete.SelectedItem != null)
            {
                Supplier selected = cbSuppliersDelete.SelectedItem as Supplier;

                using (var ctx = new OrderManagerContext())
                {                    
                    ctx.Suppliers.Remove(ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId));
                    ctx.SaveChanges();

                    dgSuppliersAdd.ItemsSource = null;
                    dgSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                    dgSuppliersEdit.ItemsSource = null;
                    dgSuppliersEdit.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersSupplierEdit.ItemsSource = ctx.Suppliers.ToList();

                    dgSuppliersDelete.ItemsSource = null;
                    dgSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();
                    cbSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();

                    dgProductsAdd.ItemsSource = null;
                    dgProductsAdd.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();
                    cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.GroupJoin(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = p.Supplier.Name }).ToList();

                }
                MessageBox.Show("De leverancier werd verwijderd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een leverancier te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_AddCustomer(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNaamCustomerAdd.Text) && !string.IsNullOrWhiteSpace(txtAdresCustomerAdd.Text))
            {
                using (var ctx = new OrderManagerContext())
                {
                    ctx.Customers.Add(new Customer() { Name = txtNaamCustomerAdd.Text, Address = txtAdresCustomerAdd.Text });
                    ctx.SaveChanges();

                    dgCustomersAdd.ItemsSource = null;
                    dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                    dgCustomersEdit.ItemsSource = null;
                    dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                    cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                    dgCustomersDelete.ItemsSource = null;
                    dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                    cbCustomersDelete.ItemsSource = ctx.Customers.ToList();
                }
                MessageBox.Show("Klant werd toegevoegd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamCustomerAdd.Clear();
                txtAdresCustomerAdd.Clear();
            }         
            else
                MessageBox.Show("Klant toevoegen mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }
                      

        private void Button_Click_EditCustomer(object sender, RoutedEventArgs e)
        {
            if (cbCustomersEdit.SelectedItem != null)
            {
                Customer selected = cbCustomersEdit.SelectedItem as Customer;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId).Name = txtNaamCustomerEdit.Text;
                    ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId).Address = txtAdresCustomerEdit.Text;
                    ctx.SaveChanges();

                    dgCustomersAdd.ItemsSource = null;
                    dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                    dgCustomersEdit.ItemsSource = null;
                    dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                    cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                    dgCustomersDelete.ItemsSource = null;
                    dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                    cbCustomersDelete.ItemsSource = ctx.Customers.ToList();

                }
                MessageBox.Show("De wijzigingen werden opgeslagen.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamCustomerEdit.Clear();
                txtAdresCustomerEdit.Clear();
            }
            else
                MessageBox.Show("Gelieve eerst een klant te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbCustomersEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer selected = cbCustomersEdit.SelectedItem as Customer;

            if (selected != null)
            {
                txtNaamCustomerEdit.Text = selected.Name;
                txtAdresCustomerEdit.Text = selected.Address;
            }
        }

        private void Button_Click_DeleteCustomer(object sender, RoutedEventArgs e)
        {
            if (cbCustomersDelete.SelectedItem != null)
            {
                Customer selected = cbCustomersDelete.SelectedItem as Customer;

                using (var ctx = new OrderManagerContext())
                {                    
                    ctx.OrderDetails.RemoveRange(ctx.OrderDetails.Where(od => od.Order.Customer.CustomerId == selected.CustomerId));
                    ctx.Orders.RemoveRange(ctx.Orders.Where(o => o.Customer.CustomerId == selected.CustomerId));
                    ctx.Customers.Remove(ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId));

                    var collection = ctx.OrderDetails.Join(ctx.Products, od => od.Product.ProductId, p => p.ProductId, (od, p) => new { od, p })
                        .Join(ctx.Customers, sc => sc.od.Order.Customer.CustomerId, cu => cu.CustomerId, (sc, cu) => new {sc, cu})
                        .Where(c => c.cu.CustomerId == selected.CustomerId).ToList();

                    foreach (var item in collection)
                    {
                        ctx.Products.FirstOrDefault(p => p.ProductId == item.sc.p.ProductId).Stock = item.sc.p.Stock + item.sc.od.Quantity;
                    }
                    ctx.SaveChanges();

                    dgCustomersAdd.ItemsSource = null;
                    dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                    dgCustomersEdit.ItemsSource = null;
                    dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                    cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                    dgCustomersDelete.ItemsSource = null;
                    dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                    cbCustomersDelete.ItemsSource = ctx.Customers.ToList();

                }
                MessageBox.Show("De klant werd verwijderd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een klant te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }
    }
}
