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
                dgMagazijn.ItemsSource = ctx.Products.Join(ctx.Suppliers,
                    p => p.Supplier.SupplierId,
                    s => s.SupplierId,
                    (p, s) => new { Name = p.Name, UnitPrice = p.UnitPrice, Stock = p.Stock, Supplier = s.Name }).ToList();
            }
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
