using MediatR;

namespace DesafioTecnico.Business.Mediator.Commands.Project
{
    public class IncluirProjetoCommandRequest : IRequest<IncluirProjetoCommandResponse>
    {
        public string Descricao { get; set; } = default!;
    }

    public class IncluirProjetoCommandResponse
    {
        public Guid Id { get; set; } = default!;
    }
}