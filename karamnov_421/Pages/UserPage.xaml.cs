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

namespace karamnov_421.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            var currentUsers = karamnovEntities1.GetContext().User.ToList();
            ListUser.ItemsSource = currentUsers;
            CmbSorting.SelectedIndex = 0;
            CheckDriver.IsChecked = false;

        }

        private void UpdateUsers()
        {
            // Загружаем всех пользователей в список
            var currentUsers = karamnovEntities1.GetContext().User.ToList();

            // Фильтрация по Ф.И.О. без учета регистра
            currentUsers = currentUsers.Where(x => x.FIO.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

            // Фильтрация по роли "пользователь"
            if (CheckDriver.IsChecked == true)
            {
                currentUsers = currentUsers.Where(x => x.Role.Contains("пользователь")).ToList();
            }

            // Сортировка по Ф.И.О.
            if (CmbSorting.SelectedIndex == 0)
            {
                currentUsers = currentUsers.OrderBy(x => x.FIO).ToList();
            }
            else
            {
                currentUsers = currentUsers.OrderByDescending(x => x.FIO).ToList();
            }

            // Обновляем отображаемые данные в ListView
            ListUser.ItemsSource = currentUsers;
        }

        private void BtnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSearch.Clear(); 
            CheckDriver.IsChecked = false; 
            CmbSorting.SelectedIndex = 0;
            UpdateUsers();
        }

        private void CheckDriver_Checked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void CmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void CheckDriver_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }
    }
}
