
using Microsoft.EntityFrameworkCore;

namespace E_commercePlants.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Page> Pages { get; set; }
    }
}
