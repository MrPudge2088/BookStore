using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cart> Carts { get; set; }
        //public DbSet<Store> Stores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WIN-MK1G1B80IQ8\\SQLEXPRESS;Initial Catalog=BookStore;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
