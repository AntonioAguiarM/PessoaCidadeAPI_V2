using Microsoft.EntityFrameworkCore;
using PessoaCidadeAPI.Models;

namespace PessoaCidadeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
               
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                .HasOne(p => p.Cidade)
                .WithMany(c => c.Habitantes)
                .HasForeignKey(p => p.IdCidadeFK)
                .HasConstraintName("ForeignKey_Pessoa_Cidade");
        }
    }
}
