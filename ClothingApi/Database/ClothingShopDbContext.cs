using ClothingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingApi.Database
{
    public class ClothingShopDbContext:DbContext
    {
        public DbSet<ClothingProduct> Product { get; set; }

        public ClothingShopDbContext(DbContextOptions<ClothingShopDbContext> options) : base(options)
        { }
    }
}
