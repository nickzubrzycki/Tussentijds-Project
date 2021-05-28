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
                dgOrders.ItemsSource = ctx.Orders.Join(ctx.Customers,
                    o => o.Customer.CustomerId,
                    c => c.CustomerId,
                    (o, c) => new { ID = o.OrderId, Klant = c.Name, Datum = o.OrderDate }).ToList();

                

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

        private void Button_Click_AddProducts(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Finish(object sender, RoutedEventArgs e)
        {

        }
    }
}