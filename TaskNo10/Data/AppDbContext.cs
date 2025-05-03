using Microsoft.EntityFrameworkCore;
using TaskNo10.Models;

namespace TaskNo10.Data{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Book table in the database
        public DbSet<Book> Books { get; set; }
    }

}