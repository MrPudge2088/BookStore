using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Carts : Window
    {
        AppDbContext db = new AppDbContext();
        User user;
        public Carts(User user)
        {
            this.user = user;
            InitializeComponent();
            BookListBox.ItemsSource = db.Carts.Include(Cart => Cart.User).Include(Cart => Cart.Book).Where(Cart => Cart.User == user).ToList();
        }
        private void UpdateBookList()
        {
            BookListBox.ItemsSource = db.Carts.Include(Cart => Cart.User).Include(Cart => Cart.Book).Where(Cart => Cart.User == user).ToList();
        }
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            List<Cart>? insideCart = db.Carts.Where(cart => cart.UserId == user.Id && cart.BookId == cart.Book.Id).ToList();
            if (insideCart.Count == 0)
            {
                MessageBox.Show("Корзина пуста, невозможно совершить покупку.");
                return;
            }
            else
            {
                foreach (var cart in insideCart) 
                {
                    List<Storage>? insideStorage = db.Storages.Where(storage => storage.BookId == cart.BookId).ToList();
                    foreach (Storage storage in insideStorage)
                    {
                        if (storage != null && (storage.Amount -= cart.Amount) >= 0)
                        {
                            storage.Amount -= cart.Amount;
                            storage.Amount += cart.Amount;
                            db.Carts.Remove(cart);
                            db.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show($"Вы пытаетесь заказать невозможное число книг с названием {cart.Book.Name}, пожалуйста проыерьте статус доступности книги и её колличество.");
                            return;
                        }
                    }
                }
                UpdateBookList();
                MessageBox.Show("Заказ был успешно выполнен");
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow client = new ClientWindow(user);
            client.Show();
            Close();
        }
    }
}