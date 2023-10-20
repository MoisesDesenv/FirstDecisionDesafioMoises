using FirstDecisionDesafioMoises.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace FirstDecisionDesafioMoises.Data
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }

        public DbSet<PessoaModel> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
                "Server = ZIZOINFO6;" +
                "DataBase = teste;" +
                "User Id = sa;" +
                "Password=12345678"
                );
    }
}