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
    /// Interaction logic for OverzichtAdmin.xaml
    /// </summary>
    public partial class OverzichtAdmin : Window
    {
        public OverzichtAdmin()
        {
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";

            using (var ctx = new OrderManagerContext())
            {
                var collectionPS = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                dgMagazijn.ItemsSource = collectionPS;

                cbSuppliers.ItemsSource = ctx.Suppliers.ToList();

                int stock = 0;
                double price = 0;

                foreach (var item in collectionPS)
                {
                    stock += item.Stock;
                    price += item.Stock * item.UnitPrice;
                }

                tbAantal.Text = stock.ToString();
                tbPrijs.Text = $"€ {price}";

                var collectionOdP = ctx.OrderDetails.Join(ctx.Products,
                    od => od.Product.ProductId,
                    p => p.ProductId,
                    (od, p) => new { od, p });

                int sales = 0;
                double revenue = 0;

                foreach (var item in collectionOdP)
                {
                    sales += item.od.Quantity;
                    revenue += item.p.UnitPrice * item.od.Quantity;
                }

                tbAfzet.Text = $"{sales} stuks";
                tbOmzet.Text = $"€ {revenue}";


                var coll = collectionOdP.GroupBy(c => c.od.Order.Customer.CustomerId);

                dgKlanten.ItemsSource = ctx.Customers.Join(coll,
                    cu => cu.CustomerId,
                    c => c.Key,
                    (cu, c) => new { Name = cu.Name, Address = cu.Address, Sales = c.Sum(s => s.od.Quantity), Revenue = c.Sum(s => s.p.UnitPrice * s.od.Quantity) }).ToList();
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }

        private void Button_Click_FilterNaam(object sender, RoutedEventArgs e)
        {
            using (var ctx = new OrderManagerContext())
            {
                if (!string.IsNullOrEmpty(txtFilterNaam.Text))
                {
                    var filter = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name })
                    .Where(f => f.Name.Contains(txtFilterNaam.Text)).ToList();

                    if (filter.Count != 0)
                    {
                        dgMagazijn.ItemsSource = null;
                        dgMagazijn.ItemsSource = filter;

                        int stock = 0;
                        double price = 0;

                        foreach (var item in filter)
                        {
                            stock += item.Stock;
                            price += item.Stock * item.UnitPrice;
                        }

                        tbAantal.Text = stock.ToString();
                        tbPrijs.Text = $"€ {price}";
                    }
                    else
                    {
                        dgMagazijn.ItemsSource = null;
                        tbAantal.Text = "0";
                        tbPrijs.Text = "0";
                        MessageBox.Show("Geen resultaat.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                }
                else
                    MessageBox.Show("Gelieve eerst een zoekterm in te vullen.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_FilterPrijs(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMinPrijs.Text))
                txtMinPrijs.Text = "0";
            if (string.IsNullOrEmpty(txtMaxPrijs.Text))
                txtMaxPrijs.Text = "0";

            double MinPrijs = double.Parse(txtMinPrijs.Text);
            double MaxPrijs = double.Parse(txtMaxPrijs.Text);

            using (var ctx = new OrderManagerContext())
            {
                var filter = ctx.Products.Join(ctx.Suppliers,
                     p => p.Supplier.SupplierId,
                     s => s.SupplierId,
                     (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name })
                     .Where(f => f.UnitPrice >= MinPrijs && f.UnitPrice <= MaxPrijs).ToList();

                if (filter.Count != 0)
                {
                    dgMagazijn.ItemsSource = null;
                    dgMagazijn.ItemsSource = filter;

                    int stock = 0;
                    double price = 0;

                    foreach (var item in filter)
                    {
                        stock += item.Stock;
                        price += item.Stock * item.UnitPrice;
                    }

                    tbAantal.Text = stock.ToString();
                    tbPrijs.Text = $"€ {price}";
                }
                else
                {
                    dgMagazijn.ItemsSource = null;
                    tbAantal.Text = "0";
                    tbPrijs.Text = "0";
                    MessageBox.Show("Geen resultaat.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void Button_Click_FilterStock(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMinStock.Text))
                txtMinStock.Text = "0";
            if (string.IsNullOrEmpty(txtMaxStock.Text))
                txtMaxStock.Text = "0";

            int MinStock = int.Parse(txtMinStock.Text);
            int MaxStock = int.Parse(txtMaxStock.Text);

            using (var ctx = new OrderManagerContext())
            {
                var filter = ctx.Products.Join(ctx.Suppliers,
                     p => p.Supplier.SupplierId,
                     s => s.SupplierId,
                     (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name })
                     .Where(f => f.Stock >= MinStock && f.Stock <= MaxStock).ToList();

                if (filter.Count != 0)
                {
                    dgMagazijn.ItemsSource = null;
                    dgMagazijn.ItemsSource = filter;

                    int stock = 0;
                    double price = 0;

                    foreach (var item in filter)
                    {
                        stock += item.Stock;
                        price += item.Stock * item.UnitPrice;
                    }

                    tbAantal.Text = stock.ToString();
                    tbPrijs.Text = $"€ {price}";
                }
                else
                {
                    dgMagazijn.ItemsSource = null;
                    tbAantal.Text = "0";
                    tbPrijs.Text = "0";
                    MessageBox.Show("Geen resultaat.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void Button_Click_FilterSupplier(object sender, RoutedEventArgs e)
        {
            Supplier selected = cbSuppliers.SelectedItem as Supplier;

            using (var ctx = new OrderManagerContext())
            {
                if (cbSuppliers.SelectedItem != null)
                {
                    var filter = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name })
                    .Where(f => f.Supplier == selected.Name).ToList();

                    if (filter.Count != 0)
                    {
                        dgMagazijn.ItemsSource = null;
                        dgMagazijn.ItemsSource = filter;

                        int stock = 0;
                        double price = 0;

                        foreach (var item in filter)
                        {
                            stock += item.Stock;
                            price += item.Stock * item.UnitPrice;
                        }

                        tbAantal.Text = stock.ToString();
                        tbPrijs.Text = $"€ {price}";
                    }
                    else
                    {
                        dgMagazijn.ItemsSource = null;
                        tbAantal.Text = "0";
                        tbPrijs.Text = "0";
                        MessageBox.Show("Geen resultaat.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                    MessageBox.Show("Gelieve eerst een leverancier te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            using (var ctx = new OrderManagerContext())
            {
                var collection = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                dgMagazijn.ItemsSource = collection;

                int stock = 0;
                double price = 0;

                foreach (var item in collection)
                {
                    stock += item.Stock;
                    price += item.Stock * item.UnitPrice;
                }

                tbAantal.Text = stock.ToString();
                tbPrijs.Text = $"€ {price}";
            }
            txtFilterNaam.Text = null;
            txtMinPrijs.Text = null;
            txtMaxPrijs.Text = null;
            txtMinStock.Text = null;
            txtMaxStock.Text = null;
            cbSuppliers.SelectedItem = null;
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            dgVerkocht.Visibility = Visibility.Hidden;
            dgKlanten.Visibility = Visibility.Visible;
            btnProducts.IsEnabled = true;
            btnCustomers.IsEnabled = false;

            using (var ctx = new OrderManagerContext())
            {

                var collection = ctx.OrderDetails.Join(ctx.Products,
                    od => od.Product.ProductId,
                    p => p.ProductId,
                    (od, p) => new { od, p })
                    .GroupBy(c => c.od.Order.Customer.CustomerId); ;

                dgKlanten.ItemsSource = ctx.Customers.Join(collection,
                    cu => cu.CustomerId,
                    c => c.Key,
                    (cu, c) => new { Name = cu.Name, Address = cu.Address, Sales = c.Sum(s => s.od.Quantity), Revenue = c.Sum(s => s.p.UnitPrice * s.od.Quantity) }).ToList();
            }
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            dgKlanten.Visibility = Visibility.Hidden;
            dgVerkocht.Visibility = Visibility.Visible;
            btnCustomers.IsEnabled = true;
            btnProducts.IsEnabled = false;

            using (var ctx = new OrderManagerContext())
            {

                dgVerkocht.ItemsSource = ctx.OrderDetails.GroupBy(od => od.Product.ProductId).Join(ctx.Products,
                    od => od.Key,
                    p => p.ProductId,
                    (od, p) => new { Name = p.Name, UnitPrice = p.UnitPrice, Sales = od.Sum(s => s.Quantity), Revenue = od.Sum(s => s.Product.UnitPrice * s.Quantity) }).ToList();


            }
        }
    }
}
