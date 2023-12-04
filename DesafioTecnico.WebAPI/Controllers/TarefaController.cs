using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Mediator.Commands.Task;
using DesafioTecnico.Business.Mediator.Queries.Task;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DesafioTecnico.WebAPI.Controllers
{
    /// <summary>
    /// Fornece métodos para genrencimento das tarefas
    /// </summary>
    [ApiController]
    [Route("api/tarefa")]
    public class TarefaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TarefaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lista todas as tarefas de um determinado projeto
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="projetoId">Identificador interno do projeto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns><see cref="IEnumerable{ListarTarefasQueryResponse}"/>Lista das tarefas do projeto</returns>
        [HttpGet]
        [Route("by-project/{projetoId}")]
        public async Task<IActionResult> ListAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid? projetoId, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new ListarTarefasQueryRequest() { ProjetoId = projetoId }, cancellationToken));

        /// <summary>
        /// Busca pelo identificador interno
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno da tarefa</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns><see cref="BuscarTarefaQueryResponse"/>Informações da tarefa</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new BuscarTarefaQueryRequest() { Id = id }, cancellationToken));

        /// <summary>
        /// Adicionar
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="request"><see cref="IncluirTarefaCommandRequest"/>Informações da tarefa</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Identificador interno da tarefa</returns>
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, IncluirTarefaCommandRequest request, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(request, cancellationToken));

        /// <summary>
        /// Altera informações
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno da tarefa</param>
        /// <param name="request"><see cref="ModificarTarefaCommandRequest"/>Informações a serem modificadas na tarefa</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, ModificarTarefaCommandRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Apagar
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new ApagarTarefaCommandRequest() { Id = id }, cancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno</param>
        /// <param name="request"><see cref="AdicionarComentarioRequest"/>Comentário</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        [HttpPost]
        [Route("{id}/comentario")]
        public async Task<IActionResult> AdicionarComentario([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, AdicionarComentarioRequest request, CancellationToken cancellationToken)
        {
            request.TarefaId = id;
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}