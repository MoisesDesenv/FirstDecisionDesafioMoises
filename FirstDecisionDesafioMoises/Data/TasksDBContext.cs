using FirstDecisionDesafioMoises.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace FirstDecisionDesafioMoises.Data
{
    public class TasksDBContext : DbContext
    {
        public TasksDBContext(DbContextOptions<TasksDBContext> options) : base(options) { }

        public DbSet<PessoaModel> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}