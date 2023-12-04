using DesafioTecnico.Business.Enums;

namespace DesafioTecnico.Business.Services.Models
{
    public class TarefaModel
    {
        public Guid Id { get; set; } = default!;
        public ProjetoModel Projeto { get; set; } = default!;
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public TarefaPrioridadeEnum Prioridade { get; set; } = default!;
        public TarefaSituacaoEnum Situacao { get; set; } = default!;
        public DateTime Criacao { get; set; } = default!;
    }
}