using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using EP.CrudModalDDD.Domain.Entities;

namespace EP.CrudModalDDD.Infra.Data.EntityConfig
{
    // Fluent API
    public class ClienteConfig : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfig()
        {
            HasKey(c => c.ClienteId);
            //HasKey(c => new {c.ClienteId, c.CPF});

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.Email.Address)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Email");

            Property(c => c.CPF.Numero)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("CPF")
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() {IsUnique = true}));

            Property(c => c.DataNascimento)
                .IsRequired();

            Property(c => c.Ativo)
                .IsRequired();

            ToTable("Clientes");
        }
    }
}