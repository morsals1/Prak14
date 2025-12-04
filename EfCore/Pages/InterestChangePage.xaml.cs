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
    /// Логика взаимодействия для InterestChangePage.xaml
    /// </summary>
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
                CurrentInterestGroup = editGroup;
                IsEdit = true;
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
                if (_service.InterestGroups.Any(u => u.Title.ToLower() == CurrentInterestGroup.Title.ToLower()))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                if (IsEdit)
                {
                    _service.Update(CurrentInterestGroup);
                    MessageBox.Show("Данные пользователя обновлены", "Успешно",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (CurrentInterestGroup.Id <= 0)
                        CurrentInterestGroup.Id = 1;

                    _service.Add(CurrentInterestGroup);
                    MessageBox.Show("Пользователь добавлен", "Успешно",
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
