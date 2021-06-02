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
    /// Interaction logic for BestellingVerkoper.xaml
    /// </summary>
    public partial class BestellingVerkoper : Window
    {
       
        public BestellingVerkoper()
        {
            InitializeComponent();                        

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";    
                                   
            using (var ctx = new OrderManagerContext())
            {
                var collection = ctx.OrderDetails.Join(ctx.Products,
                    od => od.Product.ProductId,
                    p => p.ProductId,
                    (sc, p) => new { sc, p })
                    .GroupBy(c => c.sc.Order.OrderId);

                dgOrders.ItemsSource = ctx.Orders.Join(collection,
                    o => o.OrderId,
                    c => c.Key,
                    (o, c) => new { OrderId = o.OrderId, Name = o.Customer.Name, OrderDate = o.OrderDate, TotalQ = c.Sum(s => s.sc.Quantity), TotalP = c.Sum(s => s.sc.Product.UnitPrice * s.sc.Quantity) })
                    .ToList();                    

                cbOrders.ItemsSource = ctx.Orders.ToList();
                cbCustomers.ItemsSource = ctx.Customers.ToList();
                cbProducts.ItemsSource = ctx.Products.ToList();
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }        

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Product selected = cbProducts.SelectedItem as Product;
            
            lbProducts.Items.Add(new Tuple<Product,int>(selected, Convert.ToInt32(txtAantal.Text)));

            cbProducts.SelectedItem = null;
            txtAantal.Clear();
        }

        private void Button_Click_Finish(object sender, RoutedEventArgs e)
        {
            if (cbCustomers.SelectedItem != null && dpOrder.SelectedDate != null && lbProducts.Items != null)
            {
                Customer selected = cbCustomers.SelectedItem as Customer;

                //Order order = new Order { Customer = selected, OrderDate = (DateTime)dpOrder.SelectedDate };

                //foreach (Tuple<Product, int> item in lbProducts.Items)
                //{
                //    OrderDetail detail = new OrderDetail { Order = order, Product = item.Item1, Quantity = item.Item2 };
                //    order.OrderDetail.Add(detail);
                //}

                using (var ctx = new OrderManagerContext())
                {
                    Order order = new Order { Customer = ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId), OrderDate = (DateTime)dpOrder.SelectedDate, OrderDetails = new List<OrderDetail>() };
                    

                    foreach (Tuple<Product, int> item in lbProducts.Items)
                    {
                        OrderDetail detail = new OrderDetail { Product = ctx.Products.FirstOrDefault(p => p.ProductId == item.Item1.ProductId), Quantity = item.Item2 };
                        order.OrderDetails.Add(detail);                        
                    }
                    ctx.Orders.Add(order);
                    ctx.SaveChanges();

                    dgOrders.ItemsSource = null;

                    var collection = ctx.OrderDetails.Join(ctx.Products,
                    od => od.Product.ProductId,
                    p => p.ProductId,
                    (sc, p) => new { sc, p })
                    .GroupBy(c => c.sc.Order.OrderId);

                    dgOrders.ItemsSource = ctx.Orders.Join(collection,
                        o => o.OrderId,
                        c => c.Key,
                        (o, c) => new { OrderId = o.OrderId, Name = o.Customer.Name, OrderDate = o.OrderDate, TotalQ = c.Sum(s => s.sc.Quantity), TotalP = c.Sum(s => s.sc.Product.UnitPrice * s.sc.Quantity) })
                        .ToList();

                    cbOrders.ItemsSource = ctx.Orders.ToList();
                }
                MessageBox.Show("bestelling werd aangemaakt.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                cbCustomers.SelectedItem = null;
                dpOrder.SelectedDate = null;
                lbProducts.Items.Clear();
                
            }
            else
                MessageBox.Show("bestelling aanmaken mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }

        private void Button_Click_Details(object sender, RoutedEventArgs e)
        {
            Order selected = cbOrders.SelectedItem as Order;           

            using (var ctx = new OrderManagerContext())
            {
                dgOrderDetails.ItemsSource = ctx.OrderDetails 
                    .Where(od => od.Order.OrderId == selected.OrderId)
                    .Select(od => new {Name = od.Product.Name, Quantity = od.Quantity, UnitPrice = od.Product.UnitPrice, Total = od.Product.UnitPrice * od.Quantity})
                    .ToList();
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            cbCustomers.SelectedItem = null;
            dpOrder.SelectedDate = null;
            cbProducts.SelectedItem = null;
            txtAantal.Clear();
            lbProducts.Items.Clear();
        }        

    }
}
