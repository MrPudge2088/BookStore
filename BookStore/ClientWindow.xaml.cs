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
    public partial class ClientWindow : Window
    {
        AppDbContext db = new AppDbContext();
        User user;
        public ClientWindow(User user)
        {
            this.user = user;
            InitializeComponent();
            UpdateCartText();
            CartRemoveButton.Visibility = Visibility.Hidden;
            BookListBox.ItemsSource = db.Books.Include(Book => Book.AuthorNavigation).Include(Book => Book.StorageNavigation).ToList();
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CartButton.IsEnabled = true;
            CartRemoveButton.IsEnabled = true;
            CartRemoveButton.Visibility = Visibility.Visible;
            UpdateCartAddButton();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookListBox.SelectedItem is Book book)
            {
                Cart? insideCart = db.Carts.Where(cart => cart.UserId == user.Id && cart.BookId == book.Id).FirstOrDefault();
                if (insideCart == null)
                {
                    db.Carts.Add(new Cart()
                    {
                        UserId = user.Id,
                        BookId = book.Id,
                        Amount = 1
                    });
                }
                else
                {
                    insideCart.Amount++;
                }
                db.SaveChanges();
                UpdateCartText();
                UpdateCartAddButton();
            }
            
        }
        private void CartRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookListBox.SelectedItem is Book book)
            {
                List<Cart>? insideCart = db.Carts.Where(cart => cart.UserId == user.Id && cart.BookId == book.Id).ToList();
                foreach (Cart cart in insideCart) 
                {
                    if (cart.Amount <= 0)
                    {
                        MessageBox.Show("Вы не добавили данную книгу в корзину, чтобы выполнить это действие.");
                    }
                    else
                    {
                        cart.Amount--;
                    }
                    if (cart.Amount == 0)
                    {
                        db.Carts.Remove(cart);
                    }
                    db.SaveChanges();
                    UpdateCartText();
                    UpdateCartAddButton();
                }
            }
        }
        private void UpdateCartText()
        {
            GoToCartButton.Content = $"Корзина {db.Carts.Where(cart => cart.UserId == user.Id).Sum(cart => cart.Amount)}";
        }

        private void UpdateCartAddButton()
        {
            if (BookListBox.SelectedItem is Book book)
            {
                Cart? insideCart = db.Carts.Where(cart => cart.UserId == user.Id && cart.BookId == book.Id).FirstOrDefault();
                if (insideCart == null)
                {
                    CartButton.Content = $"Добавить в корзину";
                }
                else
                {
                    CartButton.Content = $"Добавить в корзину ({insideCart.Amount})";
                }
            }
        }

        private void GoToCartButton_Click(object sender, RoutedEventArgs e)
        {
            Carts cart = new Carts(user);
            cart.Show();
            Close();
        }
    }
}