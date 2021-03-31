using DataDriven.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataDriven.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }

        //representação em memória
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}