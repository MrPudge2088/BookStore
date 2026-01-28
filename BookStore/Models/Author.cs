using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Fathername { get; set; }
        public ICollection<Book> BooksNavigation { get; set;} = new List<Book>();
    }
}
