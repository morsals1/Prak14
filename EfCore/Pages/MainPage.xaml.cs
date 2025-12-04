using EfCore.Service;
using EfCore.data;
using System;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class MainPage : Page
    {
        public UserService Service { get; set; } = new UserService();
        public User? SelectedUser { get; set; }

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Service.GetAll();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserChangePage());
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Вы действительно хотите удалить пользователя {SelectedUser.Login}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Service.Remove(SelectedUser);
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

        private void EditUser_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedUser != null)
            {
                NavigationService.Navigate(new UserChangePage(SelectedUser));
            }
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoleListPage());
        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InterestPage());
        }

        private void AddToGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}