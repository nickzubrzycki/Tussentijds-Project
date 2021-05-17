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
    /// Interaction logic for DeleteSupplier.xaml
    /// </summary>
    public partial class DeleteSupplier : Window
    {
        public DeleteSupplier()
        {
            InitializeComponent();

            using (var ctx = new OrderManagerContext())
            {
                cbSuppliers.ItemsSource = ctx.Suppliers.ToList();
            }
        }        

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Supplier selected = cbSuppliers.SelectedItem as Supplier;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Suppliers.Remove(ctx.Suppliers.FirstOrDefault(s => s.SupplierId == selected.SupplierId));
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
        
    }
}
