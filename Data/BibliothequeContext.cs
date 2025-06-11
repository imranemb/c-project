using Microsoft.EntityFrameworkCore;
using Bibliotheque.Models;

namespace Bibliotheque.Data
{
    public class BibliothequeContext : DbContext
    {
        public BibliothequeContext(DbContextOptions options) : base(options) { }

        public DbSet<Ebook> Ebooks { get; set; }

        public DbSet<PaperBook> PaperBooks { get; set; }
    }
}
