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
    /// Interaction logic for DeleteCustomer.xaml
    /// </summary>
    public partial class DeleteCustomer : Window
    {
        public DeleteCustomer()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                cbCustomers.ItemsSource = ctx.Customers.ToList();
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Customer selected = cbCustomers.SelectedItem as Customer;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Customers.Remove(ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId));
                ctx.SaveChanges();
            }
            Close();
            DataCustomers customers = new DataCustomers();
            customers.Show();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            DataCustomers customers = new DataCustomers();
            customers.Show();
        }
    }
}
