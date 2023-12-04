using DesafioTecnico.Business.Mediator.Commands.Project;
using DesafioTecnico.Business.Mediator.Commands.Task;
using DesafioTecnico.Business.Services;
using DesafioTecnico.Business.Services.Models;
using MediatR;

namespace DesafioTecnico.Business.Mediator
{
    internal class CommandHandler :
        IRequestHandler<IncluirProjetoCommandRequest, IncluirProjetoCommandResponse>,
        IRequestHandler<ModificarProjetoCommandRequest, ModificarProjetoCommandResponse>,
        IRequestHandler<ApagarProjetoCommandRequest, ApagarProjetoCommandResponse>,

        IRequestHandler<IncluirTarefaCommandRequest, IncluirTarefaCommandResponse>,
        IRequestHandler<ModificarTarefaCommandRequest, ModificarTarefaCommandResponse>,
        IRequestHandler<ApagarTarefaCommandRequest, ApagarTarefaCommandResponse>,
        IRequestHandler<AdicionarComentarioRequest, AdicionarComentarioResponse>
    {
        private readonly IProjetoService _projetoService;
        private readonly ITarefaService _tarefaService;

        public CommandHandler(
            IProjetoService projetoService,
            ITarefaService tarefaService)
        {
            _projetoService = projetoService;
            _tarefaService = tarefaService;
        }

        public async Task<IncluirProjetoCommandResponse> Handle(IncluirProjetoCommandRequest request, CancellationToken cancellationToken) =>
            new()
            {
                Id = await _projetoService.InserirOuAtualizarAsync(new() { Descricao = request.Descricao }, cancellationToken)
            };

        public async Task<ModificarProjetoCommandResponse> Handle(ModificarProjetoCommandRequest request, CancellationToken cancellationToken)
        {
            await _projetoService.InserirOuAtualizarAsync(
                new()
                {
                    Id = request.Id,
                    Descricao = request.Descricao
                },
                cancellationToken);
            return new();
        }

        public async Task<ApagarProjetoCommandResponse> Handle(ApagarProjetoCommandRequest request, CancellationToken cancellationToken)
        {
            await _projetoService.ApagarAsync(request.Id, cancellationToken);
            return new();
        }

        public async Task<IncluirTarefaCommandResponse> Handle(IncluirTarefaCommandRequest request, CancellationToken cancellationToken)
        {
            var model =
                new TarefaModel()
                {
                    Descricao = request.Descricao,
                    Prioridade = request.Prioridade,
                    Projeto = new ProjetoModel() { Id = request.ProjectId },
                    Situacao = Enums.TarefaSituacaoEnum.EmAberto,
                    Titulo = request.Titulo
                };
            return
                new()
                {
                    Id = await _tarefaService.InserirOuAlterarAsync(model, cancellationToken)
                };
        }

        public async Task<ApagarTarefaCommandResponse> Handle(ApagarTarefaCommandRequest request, CancellationToken cancellationToken)
        {
            await _tarefaService.ApagarAsync(request.Id, cancellationToken);
            return new();
        }

        public async Task<ModificarTarefaCommandResponse> Handle(ModificarTarefaCommandRequest request, CancellationToken cancellationToken)
        {
            var model =
                new TarefaModel()
                {
                    Id = request.Id,
                    Descricao = request.Descricao,
                    Titulo = request.Titulo,
                    Situacao = request.Situacao
                };
            await _tarefaService.InserirOuAlterarAsync(model, cancellationToken);
            return new();
        }

        public async Task<AdicionarComentarioResponse> Handle(AdicionarComentarioRequest request, CancellationToken cancellationToken)
        {
            await _tarefaService.AdicionarComentarioAsync(request.TarefaId, request.Comentario, cancellationToken);
            return new();
        }
    }
}