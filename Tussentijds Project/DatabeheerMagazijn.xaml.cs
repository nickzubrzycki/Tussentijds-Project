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
    /// Interaction logic for DatabeheerMagazijn.xaml
    /// </summary>
    public partial class DatabeheerMagazijn : Window
    {
        public DatabeheerMagazijn()
        {
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";

            using (var ctx = new OrderManagerContext())
            {
                dgProductsAdd.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                cbSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                dgProductsEdit.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                cbProductsEdit.ItemsSource = ctx.Products.ToList();
                cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();

                dgProductsDelete.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                cbProductsDelete.ItemsSource = ctx.Products.ToList();

                dgSuppliersAdd.ItemsSource = ctx.Suppliers.ToList();

                dgSuppliersEdit.ItemsSource = ctx.Suppliers.ToList();
                cbSuppliersSupplierEdit.ItemsSource = ctx.Suppliers.ToList();

                dgSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();
                cbSuppliersDelete.ItemsSource = ctx.Suppliers.ToList();
            }
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
                    dgProductsAdd.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                    cbProductsEdit.ItemsSource = ctx.Products.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
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
                    dgProductsAdd.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                    cbProductsEdit.ItemsSource = ctx.Products.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
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
                    ctx.Products.Remove(ctx.Products.FirstOrDefault(p => p.ProductId == selected.ProductId));
                    ctx.SaveChanges();

                    dgProductsAdd.ItemsSource = null;
                    dgProductsAdd.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                    cbProductsEdit.ItemsSource = ctx.Products.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
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
                    dgProductsAdd.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                    cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
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
                    dgProductsAdd.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                    dgProductsEdit.ItemsSource = null;
                    dgProductsEdit.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
                    cbSuppliersProductEdit.ItemsSource = ctx.Suppliers.ToList();

                    dgProductsDelete.ItemsSource = null;
                    dgProductsDelete.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                }
                MessageBox.Show("De leverancier werd verwijderd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een leverancier te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }
    }
}
