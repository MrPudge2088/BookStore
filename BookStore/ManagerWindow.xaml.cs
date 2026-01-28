using BookStore.Models;
using Microsoft.EntityFrameworkCore;
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
using BookStore.DTO;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        AppDbContext db = new AppDbContext();
        User user;
        Dictionary<string, object> tables = new Dictionary<string, object>();
        public ManagerWindow(User user)
        {
            this.user = user;
            InitializeComponent();
            tables.Add("Книги", db.Books);
            tables.Add("Авторы", db.Authors);
            tables.Add("Хранилище", db.Storages);

            TableListBox.ItemsSource = tables.Keys;
        }

        private void BookListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TableListBox.SelectedItem is not string title) 
            { return; }
            
            switch (title)
            {
                case "Книги":
                    db.Books.Load();
                    TableDataGrid.ItemsSource = db.Books.Local.ToObservableCollection();
                    break;
                case "Хранилище":
                    db.Storages.Load();
                    TableDataGrid.ItemsSource = db.Storages.Local.ToObservableCollection();
                    break;
                case "Авторы":
                    db.Authors.Load();
                    TableDataGrid.ItemsSource = db.Authors.Local.ToObservableCollection();
                    break;
            }    
        }
        private void AutoGenColumns(object sender, DataGridAutoGeneratingColumnEventArgs args)
        {
            string name = args.PropertyName;
            if (name.EndsWith("Navigation"))
            { args.Cancel =  true; }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данные успешно сохранены");
            db.SaveChanges();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderBooks order = new OrderBooks(user);
            order.Show();
            Close();
        }
    }
}