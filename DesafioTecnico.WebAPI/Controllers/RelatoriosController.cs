using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Mediator.Queries.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.WebAPI.Controllers
{
    /// <summary>
    /// Relatórios
    /// </summary>
    [ApiController]
    [Route("api/relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAutenticacaoProvider _autenticacaoProvider;

        public RelatoriosController(
            IMediator mediator,
            IAutenticacaoProvider autenticacaoProvider) : base()
        {
            _mediator = mediator;
            _autenticacaoProvider = autenticacaoProvider;
        }
        /// <summary>
        /// Relatório de tarefas concluídas por usuário (gerencial)
        /// </summary>
        /// <param name="user">Informe 0 para usuário comum ou 1 para usuário gerencial</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns></returns>

        [HttpGet]
        [Route("gerencial")]
        public async Task<IActionResult> MediaTarefasConcluidasPorUsuarioAsync([FromHeader(Name = "x-user")][Required] TipoUsuarioEnum? user, CancellationToken cancellationToken)
        {
            if (!_autenticacaoProvider.Autenticado)
                return Unauthorized();
            if (_autenticacaoProvider.Usuario != "Gerente")
                return Unauthorized();

            return Ok(await _mediator.Send(new QuantidadeTarefasFinalizadasPorMesReportRequest(), cancellationToken));
        }
    }
}