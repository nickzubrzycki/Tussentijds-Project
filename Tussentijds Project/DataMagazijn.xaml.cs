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
    /// Interaction logic for DataMagazijn.xaml
    /// </summary>
    public partial class DataMagazijn : Window
    {
        public DataMagazijn()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                dgProducts.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
            }
        }        

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            Databeheer data = new Databeheer();
            data.Show();
        }

        private void Button_Click_Suppliers(object sender, RoutedEventArgs e)
        {
            Close();
            DataSuppliers suppliers = new DataSuppliers();
            suppliers.Show();
        }
    }
}
