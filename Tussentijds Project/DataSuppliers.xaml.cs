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
    /// Interaction logic for DataSuppliers.xaml
    /// </summary>
    public partial class DataSuppliers : Window
    {
        public DataSuppliers()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            using (var ctx = new OrderManagerContext())
            {
                dgSuppliers.ItemsSource = ctx.Suppliers.Select(s => new { Name = s.Name, Address = s.Address }).ToList();
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Close();
            AddSupplier add = new AddSupplier();
            add.Show();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Close();
            EditSupplier edit = new EditSupplier();
            edit.Show();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Close();
            DeleteSupplier delete = new DeleteSupplier();
            delete.Show();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            DataMagazijn magazijn = new DataMagazijn();
            magazijn.Show();
        }
    }
}
