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
    /// Interaction logic for DataCustomers.xaml
    /// </summary>
    public partial class DataCustomers : Window
    {
        public DataCustomers()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                dgCustomers.ItemsSource = ctx.Customers.Select(c => new { Name = c.Name, Address = c.Address }).ToList();
            }
        }        

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Close();
            AddCustomer add = new AddCustomer();
            add.Show();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Close();
            EditCustomer edit = new EditCustomer();
            edit.Show();
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
    }
}
