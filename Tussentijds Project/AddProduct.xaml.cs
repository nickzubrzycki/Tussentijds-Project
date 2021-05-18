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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();


            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                cbSuppliers.ItemsSource = ctx.Suppliers.ToList();
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Supplier selected = cbSuppliers.SelectedItem as Supplier;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Products.Add(new Product() 
                { Name = txtNaam.Text, UnitPrice = Convert.ToDouble(txtPrijs.Text), Stock = Convert.ToInt32(txtAantal.Text), Supplier = ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId)});
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
