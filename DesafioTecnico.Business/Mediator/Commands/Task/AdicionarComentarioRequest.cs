using MediatR;
using System.Text.Json.Serialization;

namespace DesafioTecnico.Business.Mediator.Commands.Task
{
    public class AdicionarComentarioRequest : IRequest<AdicionarComentarioResponse>
    {
        [JsonIgnore]
        public Guid TarefaId { get; set; } = default!;
        public string Comentario { get; set; } = default!;
    }

    public class AdicionarComentarioResponse
    {

    }
}