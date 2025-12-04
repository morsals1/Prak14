using EfCore.data;
using EfCore.Service;

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

namespace EfCore.Pages
{
    /// <summary>
    /// Логика взаимодействия для InterestPage.xaml
    /// </summary>
    public partial class InterestPage : Page
    {
        public InterestService Service { get; set; } = new InterestService();
        public InterestGroup? SelectedGroup { get; set; }

        public InterestPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Service.GetAll();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Выберите пользователя для удаления!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Вы действительно хотите удалить пользователя {SelectedGroup.Id}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Service.Remove(SelectedGroup);
                    MessageBox.Show("Пользователь удален", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InterestChangePage());
        }

        private void EditInterest_Click(object sender, MouseButtonEventArgs e)
        {
            if (SelectedGroup != null)
            {
                NavigationService.Navigate(new InterestChangePage(SelectedGroup));
            }
        }
    }
}
