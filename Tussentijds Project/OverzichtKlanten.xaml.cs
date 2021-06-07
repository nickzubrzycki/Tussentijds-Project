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
    /// Interaction logic for OverzichtKlanten.xaml
    /// </summary>
    public partial class OverzichtKlanten : Window
    {
        public OverzichtKlanten()
        {
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";

            using (var ctx =  new OrderManagerContext())
            {
                var collection = ctx.OrderDetails.Join(ctx.Products,
                    od => od.Product.ProductId,
                    p => p.ProductId,
                    (od, p) => new { od, p });                    

                int quantity = 0;
                double profit = 0;

                foreach (var item in collection)
                {
                    quantity += item.od.Quantity;
                    profit += item.p.UnitPrice;
                }

                tbAfzet.Text = $"{quantity} stuks";
                tbOmzet.Text = $"€ {profit}";


                var coll = collection.GroupBy(c => c.od.Order.Customer.CustomerId);

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

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            using (var ctx = new OrderManagerContext())
            {
                
                
                dgVerkocht.ItemsSource = 
            }
        }
    }
}
