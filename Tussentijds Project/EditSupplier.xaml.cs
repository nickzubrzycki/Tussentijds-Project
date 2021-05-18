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
    /// Interaction logic for EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Window
    {
        public EditSupplier()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                cbSuppliers.ItemsSource = ctx.Suppliers.ToList();
            }
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Supplier selected = cbSuppliers.SelectedItem as Supplier;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId).Name = txtNaam.Text;
                ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId).Address = txtAdres.Text;
                ctx.SaveChanges();
            }
            Close();
            DataSuppliers suppliers = new DataSuppliers();
            suppliers.Show();

        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            DataSuppliers suppliers = new DataSuppliers();
            suppliers.Show();
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Supplier selected = cbSuppliers.SelectedItem as Supplier;

            txtNaam.Text = selected.Name;
            txtAdres.Text = selected.Address;
        }
    }
}
