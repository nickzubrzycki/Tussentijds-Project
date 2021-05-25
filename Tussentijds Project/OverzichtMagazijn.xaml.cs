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
    /// Interaction logic for OverzichtMagazijn.xaml
    /// </summary>
    public partial class OverzichtMagazijn : Window
    {
        public OverzichtMagazijn()
        {
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";            

            using (var ctx = new OrderManagerContext())
            {
                var collection = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();

                dgMagazijn.ItemsSource = collection;

                cbSuppliers.ItemsSource = ctx.Suppliers.ToList();

                int stock = 0;
                double price = 0;

                foreach(var item in collection)
                {
                    stock += item.Stock;
                    price += item.Stock * item.UnitPrice;
                }

                tbAantal.Text = stock.ToString();
                tbPrijs.Text = price.ToString();
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
                    }
                    else
                    {
                        dgMagazijn.ItemsSource = null;
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
                }
                else
                {
                    dgMagazijn.ItemsSource = null;
                    MessageBox.Show("Geen resultaat.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void Button_Click_FilterStock(object sender, RoutedEventArgs e)
        {                        
            if (string.IsNullOrEmpty(txtMinPrijs.Text))
                txtMinStock.Text = "0";
            if (string.IsNullOrEmpty(txtMaxPrijs.Text))
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
                }
                else
                {
                    dgMagazijn.ItemsSource = null;
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
                    }
                    else
                    {
                        dgMagazijn.ItemsSource = null;
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
                dgMagazijn.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
            }
            txtFilterNaam.Text = null;
            txtMinPrijs.Text = null;
            txtMaxPrijs.Text = null;
            txtMinStock.Text = null;
            txtMaxStock.Text = null;
            cbSuppliers.SelectedItem = null;
        }

        private void dgMagazijn_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
            
        }
        
    }
}
