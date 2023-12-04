using DesafioTecnico.Business.Enums;
using MediatR;

namespace DesafioTecnico.Business.Mediator.Queries.Task
{
    public class BuscarTarefaQueryRequest : IRequest<BuscarTarefaQueryResponse>
    {
        public Guid Id { get; set; } = default!;
    }

    public class BuscarTarefaQueryResponse
    {
        public Guid Id { get; set; } = default!;
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public TarefaPrioridadeEnum Prioridade { get; set; } = default!;
        public TarefaSituacaoEnum Situacao { get; set; } = default!;
        public DateTime Criacao { get; set; } = default!;
        public Guid ProjetoId { get; set; } = default!;
        public IEnumerable<HistoricoItem> Historico { get; set; } = Enumerable.Empty<HistoricoItem>();
        public class HistoricoItem
        {
            public Guid Id { get; set; } = default!;
            public DateTime Data { get; set; } = default!;
            public TipoHistoricoEnum Tipo { get; set; }
            public string Usuario { get; set; } = default!;
            public string Alteracao { get; set; } = default!;
        }
    }
}