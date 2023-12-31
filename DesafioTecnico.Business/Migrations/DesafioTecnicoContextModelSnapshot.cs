﻿// <auto-generated />
using System;
using DesafioTecnico.Business.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesafioTecnico.Business.Migrations
{
    [DbContext(typeof(DesafioTecnicoContext))]
    partial class DesafioTecnicoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.ProjetoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("DESCRICAO");

                    b.HasKey("Id");

                    b.ToTable("PROJETOS", (string)null);
                });

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.TarefaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID");

                    b.Property<DateTime>("Criacao")
                        .HasColumnType("TEXT")
                        .HasColumnName("DATA_CRIACAO");

                    b.Property<DateTime?>("DataFechamento")
                        .HasColumnType("TEXT")
                        .HasColumnName("DATA_FECHAMENTO");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT")
                        .HasColumnName("DESCRICAO");

                    b.Property<int>("Prioridade")
                        .HasColumnType("INTEGER")
                        .HasColumnName("PRIORIDADE");

                    b.Property<Guid?>("ProjetoEntityId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjetoId")
                        .HasColumnType("TEXT")
                        .HasColumnName("PROJETO_ID");

                    b.Property<int>("Situacao")
                        .HasColumnType("INTEGER")
                        .HasColumnName("SITUACAO");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("TITULO");

                    b.Property<string>("UsuarioCriacao")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT")
                        .HasColumnName("USUARIO_CRIACAO");

                    b.Property<string>("UsuarioFechamento")
                        .HasMaxLength(70)
                        .HasColumnType("TEXT")
                        .HasColumnName("USUARIO_FECHAMENTO");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoEntityId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("TAREFAS", (string)null);
                });

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.TarefaHistoricoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("ID");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("TEXT")
                        .HasColumnName("DATA");

                    b.Property<string>("Historico")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT")
                        .HasColumnName("HISTORICO");

                    b.Property<Guid>("TarefaId")
                        .HasColumnType("TEXT")
                        .HasColumnName("TAREFA_ID");

                    b.Property<int>("TipoHistorico")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TIPO_HISTORICO");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT")
                        .HasColumnName("USUARIO");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.ToTable("TAREFA_HISTORICO", (string)null);
                });

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.TarefaEntity", b =>
                {
                    b.HasOne("DesafioTecnico.Business.Commons.Entities.ProjetoEntity", null)
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoEntityId");

                    b.HasOne("DesafioTecnico.Business.Commons.Entities.ProjetoEntity", "Projeto")
                        .WithMany()
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("FK_TAREFA_PROJETO");

                    b.Navigation("Projeto");
                });

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.TarefaHistoricoEntity", b =>
                {
                    b.HasOne("DesafioTecnico.Business.Commons.Entities.TarefaEntity", "Tarefa")
                        .WithMany("Historico")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_TAREFA_HISTORICO");

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.ProjetoEntity", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("DesafioTecnico.Business.Commons.Entities.TarefaEntity", b =>
                {
                    b.Navigation("Historico");
                });
#pragma warning restore 612, 618
        }
    }
}
