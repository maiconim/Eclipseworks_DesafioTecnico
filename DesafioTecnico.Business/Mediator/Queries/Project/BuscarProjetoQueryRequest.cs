using MediatR;

namespace DesafioTecnico.Business.Mediator.Queries.Project
{
    public class BuscarProjetoQueryRequest : IRequest<BuscarProjetoQueryResponse>
    {
        public Guid Id { get; set; } = default!;
    }

    public class BuscarProjetoQueryResponse
    {
        public Guid Id { get; set; } = default!;
        public string Descricao { get; set; } = default!;
    }
}