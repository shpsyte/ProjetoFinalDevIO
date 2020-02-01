using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings {
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco> {
        public void Configure (EntityTypeBuilder<Endereco> builder) {
            builder.HasKey (p => p.Id);
            builder.Property (p => p.Street)
                .IsRequired ()
                .HasColumnName ("varchar(100)");

        }
    }
}