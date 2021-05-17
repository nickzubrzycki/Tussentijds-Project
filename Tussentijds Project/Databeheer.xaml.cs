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
    /// Interaction logic for Databeheer.xaml
    /// </summary>
    public partial class Databeheer : Window
    {
        public Databeheer()
        {
            InitializeComponent();

            lblUser.Content = $"User: {ActiveUser.FirstName} {ActiveUser.LastName}";
            lblRole.Content = $"Role: {ActiveUser.Role}";

            if (ActiveUser.Role == "Magazijnier")
            {
                btnUsers.IsEnabled = false;
                btnVerkoop.IsEnabled = false;
            }
            else if (ActiveUser.Role == "Verkoper")
            {
                btnUsers.IsEnabled = false;
                btnMagazijn.IsEnabled = false;
            }
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataUsers users = new DataUsers();
            Hide();
            users.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataMagazijn magazijn = new DataMagazijn();
            Hide();
            magazijn.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Close();
            UserMenu menu = new UserMenu();
            menu.Show();
        }
    }
}
