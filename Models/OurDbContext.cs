using ApniShop_A3.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop_A3.Models
{
    public class OurDbContext:DbContext
    {
        public DbSet<User> UserAccount { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Maker> Makers { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
