using EfCore.data;
using EfCore.Service;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class InterestPage : Page
    {
        public InterestService Service { get; set; } = new InterestService();
        public UserInterestGroupService UserInterestGroupService { get; set; } = new UserInterestGroupService();

        public InterestGroup? SelectedGroup { get; set; }
        public ObservableCollection<UserInterestGroup> GroupMembers { get; set; } = new();

        public InterestPage()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += InterestPage_Loaded;
        }

        private void InterestPage_Loaded(object sender, RoutedEventArgs e)
        {
            Service.GetAll();
            UserInterestGroupService.GetAll();
            UpdateMembersView();
        }

        private void UpdateMembersView()
        {
            if (SelectedGroup == null)
            {
                MembersTitle.Text = "Участники группы";
                GroupMembers.Clear();
                NoMembersText.Visibility = Visibility.Visible;
                GroupMembersListView.Visibility = Visibility.Collapsed;
            }
            else
            {
                MembersTitle.Text = $"Участники группы: {SelectedGroup.Title}";
                var groupMembers = UserInterestGroupService.UserInterestGroups
                    .Where(uig => uig.InterestGroupId == SelectedGroup.Id)
                    .OrderByDescending(uig => uig.IsModerator)
                    .ThenBy(uig => uig.User.Login)
                    .ToList();

                GroupMembers.Clear();
                foreach (var member in groupMembers)
                {
                    GroupMembers.Add(member);
                }
                if (GroupMembers.Count > 0)
                {
                    NoMembersText.Visibility = Visibility.Collapsed;
                    GroupMembersListView.Visibility = Visibility.Visible;
                }
                else
                {
                    NoMembersText.Visibility = Visibility.Visible;
                    GroupMembersListView.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMembersView();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Выберите группу для удаления!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBox.Show($"Вы действительно хотите удалить группу '{SelectedGroup.Title}'?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Service.Remove(SelectedGroup);
                    MessageBox.Show("Группа удалена", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    SelectedGroup = null;
                    UpdateMembersView();
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

        private void EditInterest_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (SelectedGroup != null)
            {
                NavigationService.Navigate(new InterestChangePage(SelectedGroup));
            }
        }
    }
}