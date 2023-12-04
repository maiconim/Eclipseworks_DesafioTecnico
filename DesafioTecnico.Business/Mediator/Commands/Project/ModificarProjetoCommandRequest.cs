using MediatR;
using System.Text.Json.Serialization;

namespace DesafioTecnico.Business.Mediator.Commands.Project
{
    public class ModificarProjetoCommandRequest : IRequest<ModificarProjetoCommandResponse>
    {
        [JsonIgnore]
        public Guid Id { get; set; } = default!;
        public string Descricao { get; set; } = default!;
    }

    public class ModificarProjetoCommandResponse { }
}