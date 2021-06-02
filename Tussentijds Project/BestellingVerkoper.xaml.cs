﻿using System;
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
                cbCustomersAdd.ItemsSource = ctx.Customers.ToList();
                cbProductsAdd.ItemsSource = ctx.Products.ToList();
                cbOrdersEdit.ItemsSource = ctx.Orders.ToList();
                cbCustomersEdit.ItemsSource = ctx.Customers.ToList();
                cbProductsEdit.ItemsSource = ctx.Products.ToList();
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
            if (cbProductsAdd.SelectedItem != null && !string.IsNullOrWhiteSpace(txtAantalAdd.Text))
            {
                Product selected = cbProductsAdd.SelectedItem as Product;

                if (Convert.ToInt32(txtAantalAdd.Text) <= selected.Stock)
                {
                    lbProductsAdd.Items.Add(new Tuple<Product, int>(selected, Convert.ToInt32(txtAantalAdd.Text)));
                    cbProductsAdd.SelectedItem = null;
                    txtAantalAdd.Clear();                    
                }                
                else
                    MessageBox.Show("Stock is ontoereikend voor deze bestelling!\nGelieve het aantal aan te passen.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Product toevoegen mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);            
            
        }

        private void Button_Click_Finish(object sender, RoutedEventArgs e)
        {
            if (cbCustomersAdd.SelectedItem != null && dpOrderAdd.SelectedDate != null && lbProductsAdd.Items != null)
            {
                Customer selected = cbCustomersAdd.SelectedItem as Customer;               

                using (var ctx = new OrderManagerContext())
                {
                    Order order = new Order { Customer = ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId), OrderDate = (DateTime)dpOrderAdd.SelectedDate, OrderDetails = new List<OrderDetail>() };                    

                    foreach (Tuple<Product, int> item in lbProductsAdd.Items)
                    {
                        OrderDetail detail = new OrderDetail { Product = ctx.Products.FirstOrDefault(p => p.ProductId == item.Item1.ProductId), Quantity = item.Item2 };
                        order.OrderDetails.Add(detail);
                        ctx.Products.FirstOrDefault(p => p.ProductId == detail.Product.ProductId).Stock = detail.Product.Stock - detail.Quantity;
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
                MessageBox.Show("Bestelling werd aangemaakt.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                cbCustomersAdd.SelectedItem = null;
                dpOrderAdd.SelectedDate = null;
                lbProductsAdd.Items.Clear();
                
            }
            else
                MessageBox.Show("Bestelling aanmaken mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);
            
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
            cbCustomersAdd.SelectedItem = null;
            dpOrderAdd.SelectedDate = null;
            cbProductsAdd.SelectedItem = null;
            txtAantalAdd.Clear();
            lbProductsAdd.Items.Clear();
        }

        private void cbOrdersEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order selected = cbOrdersEdit.SelectedItem as Order;

            if (cbOrdersEdit.SelectedItem != null)
            {
                using (var ctx = new OrderManagerContext())
                {
                    cbCustomersEdit.SelectedItem = selected.Customer;
                    dpOrderEdit.SelectedDate = selected.OrderDate;
                    lbProductsEdit.ItemsSource = ctx.OrderDetails.Where(od => od.Order.OrderId == selected.OrderId).Select(od => new { od.Product, od.Quantity }).ToList();
                }

            }
        }

        private void lbProductsEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tuple<Product, int> selected = lbProductsEdit.SelectedItem as Tuple<Product, int>;

            if (selected != null)
            {
                cbProductsEdit.SelectedItem = selected.Item1;
                txtAantalEdit.Text = selected.Item2.ToString();
            }

        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_CancelEdit(object sender, RoutedEventArgs e)
        {
            cbOrdersEdit.SelectedItem = null;
            cbCustomersEdit.SelectedItem = null;
            dpOrderEdit.SelectedDate = null;
            cbProductsEdit.SelectedItem = null;
            txtAantalEdit.Clear();
            lbProductsEdit.ItemsSource = null;
        }        
    }
}
