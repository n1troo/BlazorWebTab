

using BlazorWebTab.Shared;

namespace BlazorWebTab.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> contextOptions) : base (contextOptions)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
