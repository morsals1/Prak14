using EfCore.data;
using EfCore.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class MainPage : Page
    {
        public UserService Service { get; set; } = new UserService();
        public UserInterestGroupService UserInterestGroupService { get; set; } = new UserInterestGroupService();

        public User? SelectedUser { get; set; }
        public ObservableCollection<UserInterestGroup> UserGroups { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Service.GetAll();
            UserInterestGroupService.GetAll();
            UpdateGroupsView();
        }

        private void UpdateGroupsView()
        {
            if (SelectedUser == null)
            {
                GroupsTitle.Text = "Группы пользователя";
                SelectedUserInfo.Text = "Выберите пользователя слева";
                UserGroups.Clear();
                NoGroupsText.Visibility = Visibility.Visible;
                UserGroupsListView.Visibility = Visibility.Collapsed;
            }
            else
            {
                GroupsTitle.Text = $"Группы пользователя: {SelectedUser.Login}";
                SelectedUserInfo.Text = $"{SelectedUser.Name} ({SelectedUser.Email}) - {SelectedUser.Role?.Title ?? "Пользователь"}";

                // Фильтруем группы по выбранному пользователю
                var userGroups = UserInterestGroupService.UserInterestGroups
                    .Where(uig => uig.UserId == SelectedUser.Id)
                    .OrderBy(uig => uig.InterestGroup.Title)
                    .ToList();

                UserGroups.Clear();
                foreach (var group in userGroups)
                {
                    UserGroups.Add(group);
                }

                // Показываем/скрываем список групп
                if (UserGroups.Count > 0)
                {
                    NoGroupsText.Visibility = Visibility.Collapsed;
                    UserGroupsListView.Visibility = Visibility.Visible;
                }
                else
                {
                    NoGroupsText.Visibility = Visibility.Visible;
                    UserGroupsListView.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateGroupsView();
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

                    // Сбрасываем выбор
                    SelectedUser = null;
                    UpdateGroupsView();
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
            NavigationService.Navigate(new UserInterestAddPage());
        }
    }
}