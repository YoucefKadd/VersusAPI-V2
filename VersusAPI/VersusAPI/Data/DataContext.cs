using Microsoft.EntityFrameworkCore;
using VersusAPI.Models;

namespace VersusAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Equipe> Equipes { get; set; }

    }
}
