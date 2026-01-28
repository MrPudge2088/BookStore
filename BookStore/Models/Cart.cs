using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }

        public override string ToString()
        {
            return $"{Book.Name}\n    {Book.Genre}\n  \n {Book.Description}" +
                $"                                                                              Кол-во: {Amount}";
        }
    }
}
