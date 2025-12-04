using EfCore.data;
using EfCore.Service;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class UserInterestAddPage : Page
    {
        private readonly UserService _userService = new UserService();
        private readonly InterestService _interestService = new InterestService();
        private readonly UserInterestGroupService _userInterestGroupService = new UserInterestGroupService();

        public int SelectedUserId { get; set; }
        public int SelectedGroupId { get; set; }
        public string JoinedAt { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public bool IsModerator { get; set; }

        public UserInterestAddPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += UserInterestAddPage_Loaded;
        }

        private void UserInterestAddPage_Loaded(object sender, RoutedEventArgs e)
        {
            _userService.GetAll();
            _interestService.GetAll();

            UserComboBox.ItemsSource = _userService.Users;
            GroupComboBox.ItemsSource = _interestService.InterestGroups;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка валидации
            if (Validation.GetHasError(UserComboBox) ||
                Validation.GetHasError(GroupComboBox) ||
                Validation.GetHasError(JoinedAtTextBox))
            {
                MessageBox.Show("Исправьте ошибки в форме", "Ошибка валидации",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Проверка, не состоит ли уже пользователь в этой группе
            var existing = _userInterestGroupService.UserInterestGroups
                .FirstOrDefault(uig => uig.UserId == SelectedUserId &&
                                      uig.InterestGroupId == SelectedGroupId);

            if (existing != null)
            {
                MessageBox.Show("Пользователь уже состоит в этой группе!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var userInterestGroup = new UserInterestGroup
                {
                    UserId = SelectedUserId,
                    InterestGroupId = SelectedGroupId,
                    JoinedAt = JoinedAt,
                    IsModerator = IsModerator
                };

                _userInterestGroupService.Add(userInterestGroup);

                MessageBox.Show("Пользователь успешно добавлен в группу!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}