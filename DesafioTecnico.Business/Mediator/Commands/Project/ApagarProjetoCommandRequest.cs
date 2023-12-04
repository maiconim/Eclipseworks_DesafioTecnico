using MediatR;

namespace DesafioTecnico.Business.Mediator.Commands.Project
{
    public class ApagarProjetoCommandRequest : IRequest<ApagarProjetoCommandResponse>
    {
        public Guid Id { get; set; } = default!;
    }

    public class ApagarProjetoCommandResponse { }
}