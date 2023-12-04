using DesafioTecnico.Business.Enums;
using MediatR;

namespace DesafioTecnico.Business.Mediator.Commands.Task
{
    public class IncluirTarefaCommandRequest : IRequest<IncluirTarefaCommandResponse>
    {
        public Guid ProjectId { get; set; } = default!;
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public TarefaPrioridadeEnum Prioridade { get; set; } = default!;
    }

    public class IncluirTarefaCommandResponse
    {
        public Guid Id { get; set; } = default!;
    }
}