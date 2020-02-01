using System.Linq;
using Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context {
    public class MeuDbContext : DbContext {

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public MeuDbContext (DbContextOptions options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            //muda o comportamtneo padrao de colunas varchar
            foreach (var item in modelBuilder.Model.GetEntityTypes ()
                    .SelectMany (a => a.GetProperties ()
                        .Where (p => p.ClrType == typeof (string)))) {
                item.Relational ().ColumnType = "varchar(100)";

            }

            modelBuilder.ApplyConfigurationsFromAssembly (typeof (MeuDbContext).Assembly);

            base.OnModelCreating (modelBuilder);
        }
    }
}