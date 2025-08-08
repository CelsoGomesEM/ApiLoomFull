using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bloom.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.PrecoUnitario)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CategoriaId)
                .IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany() 
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property<string>("Nome_Normalizado")
                .HasColumnType("varchar(100)")
                .HasComputedColumnSql("UPPER([Nome])", stored: true)
                .IsRequired();

            builder.HasIndex("CategoriaId", "Nome_Normalizado")
                .IsUnique()
                .HasDatabaseName("UX_Produto_Categoria_Nome");
        }
    }
}
