using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioTecnico.Business.Commons.Entities.Maps
{
    internal class ProjetoEntityMap : EntityMapBase<ProjetoEntity>
    {
        public override void Configure(EntityTypeBuilder<ProjetoEntity> builder)
        {
            builder.ToTable("PROJETOS");

            builder.Property(p => p.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(150)
                .IsRequired();

            base.Configure(builder);
        }
    }
}