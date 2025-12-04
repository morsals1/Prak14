using EfCore.Service;
using EfCore.data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class RoleListPage : Page
    {
        private readonly RoleService _roleService = new();
        public ObservableCollection<Role> Roles => _roleService.Roles;
        public Role SelectedRole { get; set; }

        public RoleListPage()
        {
            InitializeComponent();
            DataContext = this;
            _roleService.GetAll();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ShowUsersForRole(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Role role)
            {
                MessageBox.Show($"Пользователи роли '{role.Title}': {role.Users.Count} чел.",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}