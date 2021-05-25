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
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {       
        public UserMenu()
        {            
            InitializeComponent();

            lblUser.Content = $"{ActiveUser.FirstName} {ActiveUser.LastName} ({ActiveUser.Role})";
        }        

        private void Button_Click_Databeheer(object sender, RoutedEventArgs e)
        {
            if (ActiveUser.Role == "Magazijnier")
            {
                DatabeheerMagazijn data = new DatabeheerMagazijn();
                Hide();
                data.Show();
            }
            else if (ActiveUser.Role == "Verkoper")
            {
                DatabeheerKlanten data = new DatabeheerKlanten();
                Hide();
                data.Show();
            }
            else
            {
                DatabeheerAdmin data = new DatabeheerAdmin();
                Hide();
                data.Show();
            }
        }

        private void Button_Click_Overzicht(object sender, RoutedEventArgs e)
        {
            if (ActiveUser.Role == "Magazijnier")
            {
                OverzichtMagazijn magazijn = new OverzichtMagazijn();
                Hide();
                magazijn.Show();
            }
            else if (ActiveUser.Role == "Verkoper")
            {
                
            }
            else
            {
                OverzichtAdmin admin = new OverzichtAdmin();
                Hide();
                admin.Show();
            }
        }

        private void Button_Click_Bestelling(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
