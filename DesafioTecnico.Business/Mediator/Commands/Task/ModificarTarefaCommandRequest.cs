using DesafioTecnico.Business.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace DesafioTecnico.Business.Mediator.Commands.Task
{
    public class ModificarTarefaCommandRequest : IRequest<ModificarTarefaCommandResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default!;
        public string Titulo { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public TarefaSituacaoEnum Situacao { get; set; } = default!;
    }

    public class ModificarTarefaCommandResponse { }
}