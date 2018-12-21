using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using EP.CrudModalDDD.Domain.Entities;

namespace EP.CrudModalDDD.Infra.Data.EntityConfig
{
    // Fluent API
    public class CidadeConfig : EntityTypeConfiguration<Cidade>
    {
        public CidadeConfig()
        {
            HasKey(c => c.Id);
            //HasKey(c => new {c.CidadeId, c.CPF});

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(c => c.CodigoIbge)
                .IsRequired()
                .HasMaxLength(100);
              
            ToTable("Cidades");
        }
    }
}