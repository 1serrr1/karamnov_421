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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void TextBoxFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHintFIO.Visibility = Visibility.Visible;
            if (TextBoxLogin.Text.Length > 0)
            {
                txtHintFIO.Visibility = Visibility.Hidden;
            }
        }
        private void PasswordBoxConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtHintPassword2.Visibility = Visibility.Visible;
            if (PasswordBoxPassword.Password.Length > 0)
            {
                txtHintPassword2.Visibility = Visibility.Hidden;
            }
        }
        private void TextBoxLogin_Changed(object sender, RoutedEventArgs e)
        {
            txtHintLogin.Visibility = Visibility.Visible;
            if (TextBoxLogin.Text.Length > 0)
            {
                txtHintLogin.Visibility = Visibility.Hidden;
            }
        }
        private void PasswordBoxPassword_Changed(object sender, RoutedEventArgs e)
        {
            txtHintPassword.Visibility = Visibility.Visible;
            if (PasswordBoxPassword.Password.Length > 0)
            {
                txtHintPassword.Visibility = Visibility.Hidden;
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxLogin.Clear();
            PasswordBoxPassword.Clear();
            PasswordBoxConfirmPassword.Clear();
            TextBoxFIO.Clear();

            NavigationService?.Navigate(new AuthPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) ||
       string.IsNullOrEmpty(PasswordBoxPassword.Password) ||
       string.IsNullOrEmpty(PasswordBoxConfirmPassword.Password) ||
       string.IsNullOrEmpty(TextBoxFIO.Text))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            using (var db = new karamnovEntities())
            {
                if (db.User.Any(u => u.Login == TextBoxLogin.Text))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }
            }

            var password = PasswordBoxPassword.Password;
            if (password.Length < 6 || !password.All(c => char.IsLetterOrDigit(c) && c <= 127) || !password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать не менее 6 символов, включать только латинские буквы и хотя бы одну цифру.");
                return;
            }


            if (password != PasswordBoxConfirmPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }

            using (var db = new karamnovEntities())
            {
                var userObject = new User
                {
                    FIO = TextBoxFIO.Text,
                    Login = TextBoxLogin.Text,
                    Password = PasswordBoxPassword.Password,
                    Role = ((ComboBoxItem)CmbRole.SelectedItem).Content.ToString()
                };

                db.User.Add(userObject);
                db.SaveChanges();
            }

            MessageBox.Show("Регистрация прошла успешно!");

            TextBoxLogin.Clear();
            PasswordBoxPassword.Clear();
            PasswordBoxConfirmPassword.Clear();
            TextBoxFIO.Clear();
            NavigationService?.Navigate(new AuthPage());

        }
    }
}