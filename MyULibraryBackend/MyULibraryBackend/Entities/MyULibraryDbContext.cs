using Microsoft.EntityFrameworkCore;
using MyULibraryBackend.Entities.Models;

namespace MyULibraryBackend.Entities
{
    public class MyULibraryDbContext : DbContext
    {
        public MyULibraryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookLog> BookLogs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
