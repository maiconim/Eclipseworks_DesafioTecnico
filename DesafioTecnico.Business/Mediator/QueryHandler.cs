using DesafioTecnico.Business.Mediator.Queries.Project;
using DesafioTecnico.Business.Mediator.Queries.Reports;
using DesafioTecnico.Business.Mediator.Queries.Task;
using DesafioTecnico.Business.Repositories;
using MediatR;

namespace DesafioTecnico.Business.Mediator
{
    internal class QueryHandler :
        IRequestHandler<ListarProjetosQueryRequest, ListarProjetosQueryResponse>,
        IRequestHandler<BuscarProjetoQueryRequest, BuscarProjetoQueryResponse>,

        IRequestHandler<ListarTarefasQueryRequest, ListarTarefasQueryResponse>,
        IRequestHandler<BuscarTarefaQueryRequest, BuscarTarefaQueryResponse>,

        IRequestHandler<QuantidadeTarefasFinalizadasPorMesReportRequest, QuantidadeTarefasFinalizadasPorMesReportResponse>
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly ITarefaRepository _tarefaRepository;

        public QueryHandler(
            IProjetoRepository projetoRepository,
            ITarefaRepository tarefaRepository)
        {
            _projetoRepository = projetoRepository;
            _tarefaRepository = tarefaRepository;
        }

        public async Task<ListarProjetosQueryResponse> Handle(ListarProjetosQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _projetoRepository.GetAllAsync(cancellationToken);
            var data = result
                .Select(s => new ListarProjetosQueryResponse.ProjetoInfoResponse()
                {
                    Id = s.Id,
                    Name = s.Descricao,
                    TotalTarefas = s.Tarefas.Count(),
                })
                .ToList();
            return new()
            {
                Data = data.ToList(),
            };
        }

        public async Task<BuscarProjetoQueryResponse> Handle(BuscarProjetoQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _projetoRepository.GetAsync(request.Id, cancellationToken)
                ?? throw new Exception($"Projeto '{request.Id}' não localizado.");
            return new()
            {
                Id = result.Id,
                Descricao = result.Descricao
            };
        }

        public async Task<BuscarTarefaQueryResponse> Handle(BuscarTarefaQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _tarefaRepository.GetAsync(request.Id, cancellationToken)
                ?? throw new Exception($"Tarefa '{request.Id}' não localizada.");
            return
                new()
                {
                    Criacao = result.Criacao,
                    Descricao = result.Descricao,
                    Id = result.Id,
                    Prioridade = result.Prioridade,
                    ProjetoId = result.ProjetoId,
                    Situacao = result.Situacao,
                    Titulo = result.Titulo,
                    Historico = result.Historico
                        .Select(s => new BuscarTarefaQueryResponse.HistoricoItem()
                        {
                            Alteracao = s.Historico,
                            Data = s.DataHora,
                            Id = s.Id,
                            Tipo = s.TipoHistorico,
                            Usuario = s.Usuario
                        })
                        .ToList()
                };
        }

        public async Task<ListarTarefasQueryResponse> Handle(ListarTarefasQueryRequest request, CancellationToken cancellationToken)
        {
            var result = request.ProjetoId == null
                ? await _tarefaRepository.GetAllAsync(cancellationToken)
                : await _tarefaRepository.GetAllAsync(request.ProjetoId.Value, cancellationToken);

            return
                new()
                {
                    Data = result
                        .Select(s =>
                            new ListarTarefasQueryResponse.TaskInfo()
                            {
                                Criacao = s.Criacao,
                                Descricao = s.Descricao,
                                Id = s.Id,
                                Prioridade = s.Prioridade,
                                ProjetoId = s.ProjetoId,
                                Situacao = s.Situacao,
                                Titulo = s.Titulo
                            })
                        .ToList()
                };
        }

        public async Task<QuantidadeTarefasFinalizadasPorMesReportResponse> Handle(QuantidadeTarefasFinalizadasPorMesReportRequest request, CancellationToken cancellationToken)
        {
            var tarefas = await _tarefaRepository.GetAllAsync(cancellationToken);
            var agrupamento = tarefas
                .Where(w => w.Situacao == Enums.TarefaSituacaoEnum.Fechada)
                .GroupBy(g => new { Ano = g.DataFechamento!.Value.Year, Mes = g.DataFechamento!.Value.Month, Usuario = g.UsuarioFechamento })
                .ToList();

            return new()
            {
                Data = agrupamento.Select(s => new QuantidadeTarefasFinalizadasPorMesReportResponse.DataInfo() { Ano = s.Key.Ano, Mes = s.Key.Mes, Usuario = s.Key.Usuario, TotalTarefasConcluidas = s.Count() })
                .ToList()
            };
        }
    }
}