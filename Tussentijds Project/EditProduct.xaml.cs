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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        public EditProduct()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                cbProducts.ItemsSource = ctx.Products.ToList();
                cbSuppliers.ItemsSource = ctx.Suppliers.ToList();
            }
        }

        private void cbProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product selected = cbProducts.SelectedItem as Product;

            txtNaam.Text = selected.Name;
            txtPrijs.Text = selected.UnitPrice.ToString();
            txtAantal.Text = selected.Stock.ToString();
            cbSuppliers.SelectedItem = selected.Supplier;
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = cbProducts.SelectedItem as Product;
            Supplier selectedSupplier = cbSuppliers.SelectedItem as Supplier;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).Name = txtNaam.Text;
                ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).UnitPrice = Convert.ToDouble(txtPrijs.Text);
                ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).Stock = Convert.ToInt32(txtAantal.Text);
                ctx.Products.FirstOrDefault(p => p.ProductId == selectedProduct.ProductId).Supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selectedSupplier.SupplierId);
                ctx.SaveChanges();
            }
            Close();
            DataMagazijn magazijn = new DataMagazijn();
            magazijn.Show();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            DataMagazijn magazijn = new DataMagazijn();
            magazijn.Show();
        }
    }
}
