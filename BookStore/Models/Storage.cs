using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public Book BookNavigation { get; set; }
    }
}
