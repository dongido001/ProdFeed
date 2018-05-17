using Microsoft.EntityFrameworkCore;

namespace ProdFeed.Models
{
    public class ProdFeedContext : DbContext
    {
        public ProdFeedContext (DbContextOptions<ProdFeedContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}