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
    /// Interaction logic for DatabeheerKlanten.xaml
    /// </summary>
    public partial class DatabeheerKlanten : Window
    {
        public DatabeheerKlanten()
        {
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";


            using (var ctx = new OrderManagerContext())
            {
                dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                cbCustomersDelete.ItemsSource = ctx.Customers.ToList();
            }
        }

        private void Button_Click_AddCustomer(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNaamCustomerAdd.Text) && !string.IsNullOrWhiteSpace(txtAdresCustomerAdd.Text))
            {
                using (var ctx = new OrderManagerContext())
                {
                    ctx.Customers.Add(new Customer() { Name = txtNaamCustomerAdd.Text, Address = txtAdresCustomerAdd.Text });
                    ctx.SaveChanges();

                    dgCustomersAdd.ItemsSource = null;
                    dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                    dgCustomersEdit.ItemsSource = null;
                    dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                    cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                    dgCustomersDelete.ItemsSource = null;
                    dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                    cbCustomersDelete.ItemsSource = ctx.Customers.ToList();
                }
                MessageBox.Show("Klant werd toegevoegd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamCustomerAdd.Clear();
                txtAdresCustomerAdd.Clear();
            }
            else
                MessageBox.Show("Klant toevoegen mislukt!\nNiet alle velden werden ingevuld.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_EditCustomer(object sender, RoutedEventArgs e)
        {
            if (cbCustomersEdit.SelectedItem != null)
            {
                Customer selected = cbCustomersEdit.SelectedItem as Customer;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId).Name = txtNaamCustomerEdit.Text;
                    ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId).Address = txtAdresCustomerEdit.Text;
                    ctx.SaveChanges();

                    dgCustomersAdd.ItemsSource = null;
                    dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                    dgCustomersEdit.ItemsSource = null;
                    dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                    cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                    dgCustomersDelete.ItemsSource = null;
                    dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                    cbCustomersDelete.ItemsSource = ctx.Customers.ToList();

                }
                MessageBox.Show("De wijzigingen werden opgeslagen.", "", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNaamCustomerEdit.Clear();
                txtAdresCustomerEdit.Clear();
            }
            else
                MessageBox.Show("Gelieve eerst een klant te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void cbCustomersEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Customer selected = cbCustomersEdit.SelectedItem as Customer;

            if (selected != null)
            {
                txtNaamCustomerEdit.Text = selected.Name;
                txtAdresCustomerEdit.Text = selected.Address;
            }
        }

        private void Button_Click_DeleteCustomer(object sender, RoutedEventArgs e)
        {
            if (cbCustomersDelete.SelectedItem != null)
            {
                Customer selected = cbCustomersDelete.SelectedItem as Customer;

                using (var ctx = new OrderManagerContext())
                {
                    ctx.Customers.Remove(ctx.Customers.FirstOrDefault(c => c.CustomerId == selected.CustomerId));
                    ctx.SaveChanges();

                    dgCustomersAdd.ItemsSource = null;
                    dgCustomersAdd.ItemsSource = ctx.Customers.ToList();

                    dgCustomersEdit.ItemsSource = null;
                    dgCustomersEdit.ItemsSource = ctx.Customers.ToList();
                    cbCustomersEdit.ItemsSource = ctx.Customers.ToList();

                    dgCustomersDelete.ItemsSource = null;
                    dgCustomersDelete.ItemsSource = ctx.Customers.ToList();
                    cbCustomersDelete.ItemsSource = ctx.Customers.ToList();

                }
                MessageBox.Show("De klant werd verwijderd.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Gelieve eerst een klant te selecteren.", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }
    }

    
}
