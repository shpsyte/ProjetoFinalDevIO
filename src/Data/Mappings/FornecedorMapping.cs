using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings {
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor> {
        public void Configure (EntityTypeBuilder<Fornecedor> builder) {
            builder.HasKey (p => p.Id);
            builder.Property (p => p.Name)
                .IsRequired ()
                .HasColumnName ("varchar(100)");

            builder.HasOne (p => p.Endereco)
                .WithOne (a => a.Fornecedor);

            builder.HasMany (p => p.Produtos)
                .WithOne (a => a.Fornecedor)
                .HasForeignKey (a => a.FornecedorId);

        }
    }
}