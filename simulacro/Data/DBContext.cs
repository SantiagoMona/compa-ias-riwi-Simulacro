using Microsoft.EntityFrameworkCore;
using simulacro.Models;

namespace simulacro.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options){}
        public DbSet<Company> Companies { get; set; }
        public DbSet<Sector> Sectors { get; set; }

    }
}