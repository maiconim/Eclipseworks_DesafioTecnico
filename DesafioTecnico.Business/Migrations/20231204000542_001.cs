using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioTecnico.Business.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROJETOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DESCRICAO = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJETOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TAREFAS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    PROJETO_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TITULO = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    DESCRICAO = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                    PRIORIDADE = table.Column<int>(type: "INTEGER", nullable: false),
                    SITUACAO = table.Column<int>(type: "INTEGER", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "TEXT", nullable: false),
                    USUARIO_CRIACAO = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    USUARIO_FECHAMENTO = table.Column<string>(type: "TEXT", maxLength: 70, nullable: true),
                    DATA_FECHAMENTO = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProjetoEntityId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREFAS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TAREFAS_PROJETOS_ProjetoEntityId",
                        column: x => x.ProjetoEntityId,
                        principalTable: "PROJETOS",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TAREFA_PROJETO",
                        column: x => x.PROJETO_ID,
                        principalTable: "PROJETOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TAREFA_HISTORICO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    TAREFA_ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    DATA = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TIPO_HISTORICO = table.Column<int>(type: "INTEGER", nullable: false),
                    USUARIO = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    HISTORICO = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAREFA_HISTORICO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TAREFA_HISTORICO",
                        column: x => x.TAREFA_ID,
                        principalTable: "TAREFAS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAREFA_HISTORICO_TAREFA_ID",
                table: "TAREFA_HISTORICO",
                column: "TAREFA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TAREFAS_PROJETO_ID",
                table: "TAREFAS",
                column: "PROJETO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TAREFAS_ProjetoEntityId",
                table: "TAREFAS",
                column: "ProjetoEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAREFA_HISTORICO");

            migrationBuilder.DropTable(
                name: "TAREFAS");

            migrationBuilder.DropTable(
                name: "PROJETOS");
        }
    }
}
