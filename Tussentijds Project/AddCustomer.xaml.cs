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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public AddCustomer()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            using (var ctx = new OrderManagerContext())
            {
                ctx.Customers.Add(new Customer() { Name = txtNaam.Text, Address = txtAdres.Text });
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
