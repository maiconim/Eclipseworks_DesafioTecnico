using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Mediator.Commands.Project;
using DesafioTecnico.Business.Mediator.Queries.Project;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.WebAPI.Controllers
{
    /// <summary>
    /// Fornece métodos para gerencimaneto dos projetos
    /// </summary>
    [ApiController]
    [Route("api/projeto")]
    public class ProjetoController : ControllerBase
    {
        private readonly IMediator _mediator;

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        public ProjetoController(IMediator mediator)
        {
            _mediator = mediator;
        }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>
        /// Listar todos os projetos
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns><see cref="IEnumerable{ListarProjetosQueryResponse}"/>Lista com os projetos</returns>
        [HttpGet]
        public async Task<IActionResult> ListAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new ListarProjetosQueryRequest(), cancellationToken));

        /// <summary>
        /// Busca projeto pelo identificador interno
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno do projeto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns><see cref="BuscarProjetoQueryResponse"/>Detalhes do projeto</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(new BuscarProjetoQueryRequest() { Id = id }, cancellationToken));

        /// <summary>
        /// Adiciona um novo projeto
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="request"><see cref="IncluirProjetoCommandRequest"/>Informações do projeto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns>Identificador interno do projeto</returns>
        [HttpPost]
        public async Task<IActionResult> IncluirAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, IncluirProjetoCommandRequest request, CancellationToken cancellationToken) =>
            Ok(await _mediator.Send(request, cancellationToken));

        /// <summary>
        /// Modifica informações do projeto
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno do projeto</param>
        /// <param name="request"><see cref="ModificarProjetoCommandRequest"/>Informações a serem modificadas no projeto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> AlterarAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, ModificarProjetoCommandRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request, cancellationToken));
        }

        /// <summary>
        /// Apaga um projeto
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="id">Identificador interno do projeto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> ApagarAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, Guid id, CancellationToken cancellationToken)
        {
            var request = new ApagarProjetoCommandRequest { Id = id };
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}