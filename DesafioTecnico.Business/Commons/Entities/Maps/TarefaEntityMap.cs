using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DesafioTecnico.Business.Commons.Entities.Maps
{
    internal class TarefaEntityMap : EntityMapBase<TarefaEntity>
    {
        public override void Configure(EntityTypeBuilder<TarefaEntity> builder)
        {
            builder.ToTable("TAREFAS");

            builder.Property(p => p.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(p => p.Criacao)
                .HasColumnName("DATA_CRIACAO")
                .IsRequired();

            builder.Property(p => p.Prioridade)
                .HasColumnName("PRIORIDADE")
                .IsRequired();

            builder.Property(p => p.Situacao)
                .HasColumnName("SITUACAO")
                .IsRequired();

            builder.Property(p => p.UsuarioCriacao)
                .HasColumnName("USUARIO_CRIACAO")
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(p => p.UsuarioFechamento)
                .HasColumnName("USUARIO_FECHAMENTO")
                .HasMaxLength(70);

            builder.Property(p => p.DataFechamento)
                .HasColumnName("DATA_FECHAMENTO");

            builder.Property(p => p.ProjetoId)
                .HasColumnName("PROJETO_ID")
                .IsRequired();

            builder.HasOne(p => p.Projeto)
                .WithMany()
                .HasForeignKey(p => p.ProjetoId)
                .HasConstraintName("FK_TAREFA_PROJETO")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Historico)
                .WithOne(p => p.Tarefa)
                .HasForeignKey(p => p.TarefaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TAREFA_HISTORICO");

            base.Configure(builder);
        }
    }
}