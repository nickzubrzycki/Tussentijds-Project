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
    /// Interaction logic for DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        public DeleteUser()
        {
            InitializeComponent();

            using (var ctx = new OrderManagerContext())
            {
                cbUsers.ItemsSource = ctx.Users.ToList();
            }
        }

        private void cbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User selected = cbUsers.SelectedItem as User;

            using (var ctx = new OrderManagerContext())
            {
                ctx.Users.Remove(ctx.Users.FirstOrDefault(u => u.UserId == selected.UserId));
                ctx.SaveChanges();
            }
            Close();
            DataUsers users = new DataUsers();
            users.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            DataUsers users = new DataUsers();
            users.Show();
        }
    }
}
