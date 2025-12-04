using EfCore.data;
using EfCore.Service;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EfCore.Pages
{
    public partial class InterestChangePage : Page
    {
        private InterestService _service = new InterestService();
        public InterestGroup CurrentInterestGroup { get; set; }
        public bool IsEdit { get; set; } = false;

        public InterestChangePage(InterestGroup? editGroup = null)
        {
            InitializeComponent();

            if (editGroup != null)
            {
                CurrentInterestGroup = new InterestGroup
                {
                    Id = editGroup.Id,
                    Title = editGroup.Title,
                    Description = editGroup.Description
                };
                IsEdit = true;
            }
            else
            {
                CurrentInterestGroup = new InterestGroup();
            }

            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(NameTextBox) ||
                Validation.GetHasError(DescTextBox))
            {
                MessageBox.Show("Исправьте ошибки в форме", "Ошибка валидации",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsEdit)
            {
                if (_service.InterestGroups.Any(g =>
                    g.Title.ToLower() == CurrentInterestGroup.Title.Trim().ToLower()))
                {
                    MessageBox.Show("Группа с таким названием уже существует", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                if (_service.InterestGroups.Any(g =>
                    g.Id != CurrentInterestGroup.Id &&
                    g.Title.ToLower() == CurrentInterestGroup.Title.Trim().ToLower()))
                {
                    MessageBox.Show("Группа с таким названием уже существует", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                if (IsEdit)
                {
                    _service.Update(CurrentInterestGroup);
                    MessageBox.Show("Данные группы обновлены", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _service.Add(CurrentInterestGroup);
                    MessageBox.Show("Группа добавлена", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}