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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        AppDbContext db = new AppDbContext();
        public Login()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            User user = db.Users.Where(user => user.Email.Equals(email) && user.Password.Equals(password)).FirstOrDefault();

            if (email.IsNullOrEmpty() || password.IsNullOrEmpty()) 
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    ClientWindow client = new ClientWindow(user);
                    client.Show();
                    Close();
                }
                else if (user.RoleId == 3)
                {
                    AdminWindow admin = new AdminWindow(user);
                    admin.Show();
                    Close();
                }
                else if (user.RoleId == 4) 
                {
                    ManagerWindow manager = new ManagerWindow(user);
                    manager.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Ошибка логина или пароля");
            }
        }

    private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
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
