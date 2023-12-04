using DesafioTecnico.Business.Enums;
using MediatR;

namespace DesafioTecnico.Business.Mediator.Queries.Task
{
    public class ListarTarefasQueryRequest : IRequest<ListarTarefasQueryResponse>
    {
        public Guid? ProjetoId { get; set; }
    }

    public class ListarTarefasQueryResponse
    {
        public IList<TaskInfo> Data { get; set; } = new List<TaskInfo>();
        public int Count => Data.Count;

        public class TaskInfo
        {
            public Guid Id { get; set; } = default!;
            public string Titulo { get; set; } = default!;
            public string Descricao { get; set; } = default!;
            public TarefaPrioridadeEnum Prioridade { get; set; } = default!;
            public TarefaSituacaoEnum Situacao { get; set; } = default!;
            public DateTime Criacao { get; set; } = default!;
            public Guid ProjetoId { get; set; } = default!;
        }
    }
}