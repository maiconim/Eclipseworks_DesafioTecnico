using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioTecnico.Business.Commons.Entities.Maps
{
    internal class TarefaHistoricoEntityMap:EntityMapBase<TarefaHistoricoEntity>
    {
        public override void Configure(EntityTypeBuilder<TarefaHistoricoEntity> builder)
        {
            builder.ToTable("TAREFA_HISTORICO");

            builder.Property(p => p.TarefaId)
                .HasColumnName("TAREFA_ID")
                .IsRequired();

            builder.Property(p => p.TipoHistorico)
                .HasColumnName("TIPO_HISTORICO")
                .IsRequired();

            builder.Property(p => p.DataHora)
                .HasColumnName("DATA")
                .IsRequired();

            builder.Property(p => p.Historico)
                .HasColumnName("HISTORICO")
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(p => p.Usuario)
                .HasColumnName("USUARIO")
                .HasMaxLength(70)
                .IsRequired();

            base.Configure(builder);
        }
    }
}