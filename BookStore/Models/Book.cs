using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; } 
        public int AuthorId { get; set; }
        public Storage StorageNavigation { get; set; }
        public Author AuthorNavigation { get; set; }

        public override string ToString()
        {

            return $"{Name}\n    {Genre}\n  {AuthorNavigation.Name} {AuthorNavigation.Fathername} {AuthorNavigation.Surname} \n {Description}" +
                $"                                           Кол-во на складе: {StorageNavigation.Amount},   {(StorageNavigation.Amount>0?"В наличии":"Нет в Наличии")}";
        }
    }
}
