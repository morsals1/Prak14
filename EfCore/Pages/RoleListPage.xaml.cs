using EfCore.data;
using EfCore.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class RoleListPage : Page
    {
        private readonly RoleService _roleService = new RoleService();
        private readonly UserService _userService = new UserService();

        public ObservableCollection<Role> Roles { get; set; } = new();
        public ObservableCollection<User> RoleUsers { get; set; } = new();
        public Role SelectedRole { get; set; }

        public RoleListPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += RoleListPage_Loaded;
        }

        private void RoleListPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _roleService.GetAll();
                _userService.GetAll();
                Roles.Clear();
                foreach (var role in _roleService.Roles)
                {
                    Roles.Add(role);
                }

                UpdateUsersView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUsersView()
        {
            if (SelectedRole == null)
            {
                UsersTitle.Text = "Пользователи роли";
                SelectedRoleInfo.Text = "Выберите роль слева";
                RoleUsers.Clear();
                NoUsersText.Visibility = Visibility.Visible;
                RoleUsersListView.Visibility = Visibility.Collapsed;
                return;
            }

            UsersTitle.Text = $"Пользователи роли: {SelectedRole.Title}";
            SelectedRoleInfo.Text = $"Роль: {SelectedRole.Title}";
            var usersInRole = _userService.Users
                .Where(u => u.RoleId == SelectedRole.Id)
                .ToList();

            RoleUsers.Clear();
            foreach (var user in usersInRole)
            {
                RoleUsers.Add(user);
            }
            if (RoleUsers.Count > 0)
            {
                NoUsersText.Visibility = Visibility.Collapsed;
                RoleUsersListView.Visibility = Visibility.Visible;
            }
            else
            {
                NoUsersText.Visibility = Visibility.Visible;
                RoleUsersListView.Visibility = Visibility.Collapsed;
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsersView();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}