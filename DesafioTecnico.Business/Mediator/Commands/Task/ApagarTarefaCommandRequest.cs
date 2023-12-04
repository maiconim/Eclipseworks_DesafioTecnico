using MediatR;

namespace DesafioTecnico.Business.Mediator.Commands.Task
{
    public class ApagarTarefaCommandRequest : IRequest<ApagarTarefaCommandResponse>
    {
        public Guid Id { get; set; } = default!;
    }

    public class ApagarTarefaCommandResponse { }
}