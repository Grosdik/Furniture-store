using Furniture_store.Database;
using Furniture_store.Utils;
using Furniture_store.Views.AddEditPage;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Furniture_store.Views.Control_Page
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private List<Clients> Clients;
        public ManagerPage()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
                if (Visibility == Visibility.Visible)
                {
                    Furniture_storeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    dataGridClients.ItemsSource = Furniture_storeEntities.GetContext().Clients.ToList();
                }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerAddEditClientPage((sender as Button).DataContext as Clients));
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerAddEditClientPage(null));
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var clientForRemoving = dataGridClients.SelectedItems.Cast<Clients>().ToList();

            if(MessageBox.Show($"Вы точно хотите удалить следующие {clientForRemoving.Count()} элементов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Furniture_storeEntities.GetContext().Clients.RemoveRange(clientForRemoving);
                    Furniture_storeEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    dataGridClients.ItemsSource = Furniture_storeEntities.GetContext().Clients.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void txtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGridClients.ItemsSource = Furniture_storeEntities.GetContext().Clients.ToList().Where(p => p.Surname.ToLower().Contains(txtBoxSearch.Text.ToLower())).ToList();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            Transfer.MainFrame.Navigate(new ManagerProductPage());
        }
    }
}
