using MediatR;
using System.Text.Json.Serialization;

namespace DesafioTecnico.Business.Mediator.Commands.Task
{
    /// <summary>
    /// 
    /// </summary>
    public class AdicionarComentarioRequest : IRequest<AdicionarComentarioResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Guid TarefaId { get; set; } = default!;
        /// <summary>
        /// 
        /// </summary>
        public string Comentario { get; set; } = default!;
    }

    /// <summary>
    /// 
    /// </summary>
    public class AdicionarComentarioResponse
    {

    }
}