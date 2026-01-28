using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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
    public partial class MainWindow : Window
    {
        AppDbContext db = new AppDbContext();
        public List<Book> BookList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            BookList = db.Books.Include(Book => Book.AuthorNavigation).Include(Book => Book.StorageNavigation).ToList();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            Hide();
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            Hide();
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CartButton.IsEnabled = true;
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            Close();
        }
    }
}