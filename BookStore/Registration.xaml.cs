using BookStore.Models;
using Microsoft.IdentityModel.Tokens;
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
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        AppDbContext dbContext = new AppDbContext();
        MainWindow main = new MainWindow();
        public Registration()
        {
            InitializeComponent();
            ErrorLabel.Visibility = Visibility.Hidden;
        }

        private void RegistButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordTextBox.Text;
            string confirmPasswords = PasswordConformationTextBox.Text;

            if (email.IsNullOrEmpty() || password.IsNullOrEmpty() || confirmPasswords.IsNullOrEmpty() || NameTextBox.Text.IsNullOrEmpty() || SurnameTextBox.Text.IsNullOrEmpty())
            {
                ErrorLabel.Content = "Заполните все обязательные поля";
                ErrorLabel.Visibility = Visibility.Visible;
            }
            else
            {
                if (confirmPasswords == password)
                {
                    if (dbContext.Users.Where(emailInBd => emailInBd.Email == email).FirstOrDefault() == null)
                    {
                        User user = new User()
                        {
                            Name = NameTextBox.Text,
                            Surname = SurnameTextBox.Text,
                            Fathername = FathernameTextBox.Text,
                            RoleId = 1,
                            Email = email,
                            Password = password
                        };
                        dbContext.Users.Add(user);
                        dbContext.SaveChanges();
                        Hide();
                        main.Show();
                    }
                    else
                    {
                        ErrorLabel.Content = "Такой пользователь существует";
                        ErrorLabel.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    ErrorLabel.Content = "Пароли не совпадают";
                    ErrorLabel.Visibility = Visibility.Visible;
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
