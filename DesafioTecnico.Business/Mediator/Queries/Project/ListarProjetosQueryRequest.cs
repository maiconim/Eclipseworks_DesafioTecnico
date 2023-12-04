using MediatR;

namespace DesafioTecnico.Business.Mediator.Queries.Project
{
    public class ListarProjetosQueryRequest : IRequest<ListarProjetosQueryResponse>
    {
    }

    public class ListarProjetosQueryResponse
    {
        public IEnumerable<ProjetoInfoResponse> Data { get; set; } = Enumerable.Empty<ProjetoInfoResponse>();
        public int TotalRegistros => Data?.Count() ?? 0;
        public class ProjetoInfoResponse
        {
            public Guid Id { get; set; } = Guid.Empty;
            public string Name { get; set; } = default!;
            public int TotalTarefas { get; set; } = 0;
        }
    }

}