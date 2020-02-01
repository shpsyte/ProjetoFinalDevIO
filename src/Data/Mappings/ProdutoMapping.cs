using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
  public class ProdutoMapping : IEntityTypeConfiguration<Produto> {
        public void Configure (EntityTypeBuilder<Produto> builder) {
            builder.HasKey (p => p.Id);
            builder.Property (p => p.Name)
                .IsRequired ()
                .HasColumnName ("varchar(100)");

        }
    }
}