using BookStore.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для OrderBooks.xaml
    /// </summary>
    public partial class OrderBooks : Window
    {
        AppDbContext db = new AppDbContext();
        User user;
        public OrderBooks(User user)
        {
            this.user = user;
            InitializeComponent();
            AmountLabel.Visibility = Visibility.Hidden;
            AmountTextBox.Visibility = Visibility.Hidden;
            OrderButton.Visibility = Visibility.Hidden;
            BookStoreListBox.ItemsSource = db.Books.Include(Book => Book.AuthorNavigation).Include(Book => Book.StorageNavigation).ToList();
        }
        private void UpdateStoreContent()
        {
            BookStoreListBox.ItemsSource = db.Books.Include(Book => Book.AuthorNavigation).Include(Book => Book.StorageNavigation).ToList();
        }

        private void BookStoreListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AmountLabel.Visibility = Visibility.Visible;
            AmountTextBox.Visibility = Visibility.Visible;
            OrderButton.Visibility = Visibility.Visible;
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int OrderedAmount = int.Parse(AmountTextBox.Text);
                if (BookStoreListBox.SelectedItem is Book book)
                {
                    Storage? insideStorage = db.Storages.Where(storage => storage.BookId == book.Id).FirstOrDefault();
                    if (insideStorage != null)
                    {
                        insideStorage.Amount += Math.Abs(OrderedAmount);
                    }
                    UpdateStoreContent();
                    MessageBox.Show("Заказ был успешно выполнен");
                    db.SaveChanges();
                }
            } 
            catch
            {
                MessageBox.Show("Пожалуйста, введите число в корректном формате.");
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            if (user.RoleId == 3)
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
    }
}
