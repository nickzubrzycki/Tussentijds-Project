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
    /// Interaction logic for DeleteProduct.xaml
    /// </summary>
    public partial class DeleteProduct : Window
    {
        public DeleteProduct()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                cbProducts.ItemsSource = ctx.Products.ToList();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Product selected = cbProducts.SelectedItem as Product;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Products.Remove(ctx.Products.FirstOrDefault(p => p.ProductId == selected.ProductId));
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
