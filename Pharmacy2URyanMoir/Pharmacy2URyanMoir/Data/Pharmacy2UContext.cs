using Microsoft.EntityFrameworkCore;
using Pharmacy2URyanMoir.Models;

namespace Pharmacy2URyanMoir.Data
{
    public class Pharmacy2UContext : DbContext
    {
        public DbSet<Currencies> Currencies { get; set; }
        public DbSet<ExchangeRates> ExchangeRates { get; set; }
        public DbSet<Logs> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
