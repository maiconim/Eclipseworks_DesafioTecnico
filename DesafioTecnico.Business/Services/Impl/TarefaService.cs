using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Enums;
using DesafioTecnico.Business.Repositories;
using DesafioTecnico.Business.Services.Models;
using System.Text;

namespace DesafioTecnico.Business.Services.Impl
{
    internal class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly ITarefaHistoricoRepository _tarefaHistoricoRepository;
        private readonly IAutenticacaoProvider _autenticacaoProvider;

        public TarefaService(
            IProjetoRepository projetoRepository,
            ITarefaRepository tarefaRepository,
            ITarefaHistoricoRepository tarefaHistoricoRepository,
            IAutenticacaoProvider autenticacaoProvider)
        {
            _projetoRepository = projetoRepository;
            _tarefaRepository = tarefaRepository;
            _tarefaHistoricoRepository = tarefaHistoricoRepository;
            _autenticacaoProvider = autenticacaoProvider;
        }

        public async Task<Guid> InserirOuAlterarAsync(TarefaModel tarefa, CancellationToken cancellationToken)
        {
            var entity = await _tarefaRepository.GetAsync(tarefa.Id, cancellationToken);
            if (tarefa.Id == Guid.Empty && entity == null)
            {
                var projeto = await _projetoRepository.GetAsync(tarefa.Projeto.Id, cancellationToken)
                        ?? throw new Exception($"Projeto '{tarefa.Projeto.Id}' não encontrado.");

                if (tarefa.Id == Guid.Empty && projeto.Tarefas.Count > 19)
                    throw new Exception("Cada projeto pode conter no máximo 20 tarefas.");

                entity = new TarefaEntity(projeto, tarefa.Titulo, tarefa.Descricao, tarefa.Prioridade, tarefa.Situacao, _autenticacaoProvider.Usuario);
            }
            else if (entity == null)
                throw new Exception($"Tarefa '{tarefa.Id}' não localizada.");

            if (entity!.Id != Guid.Empty)
                entity
                    .AlterarSituacao(tarefa.Situacao, _autenticacaoProvider.Usuario)
                    .AlterarTitulo(tarefa.Titulo)
                    .AlterarDescricao(tarefa.Descricao);

            await _tarefaRepository.InserirOuAtualizarAsync(entity, cancellationToken);

            if (tarefa.Id != Guid.Empty && entity.Changes.Any())
            {
                var sb = new StringBuilder();
                foreach (var item in entity.Changes)
                    sb.AppendLine($"Alterado '{item.Key}' de '{item.Value.OldValue}' para '{item.Value.NewValue}'.");

                await _tarefaHistoricoRepository.InserirOuAtualizarAsync(
                    new(entity, TipoHistoricoEnum.Alteracao, sb.ToString(), _autenticacaoProvider.Usuario?.ToString() ?? string.Empty),
                    cancellationToken);
            }

            return entity.Id;
        }

        public async Task ApagarAsync(Guid tarefaID, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.GetAsync(tarefaID, cancellationToken)
                ?? throw new Exception($"Tarefa '{tarefaID}' não localizada.");

            await _tarefaRepository.ApagarAsync(tarefa, cancellationToken);
        }

        public async Task AdicionarComentarioAsync(Guid tarefaID, string comentario, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.GetAsync(tarefaID, cancellationToken)
                ?? throw new Exception($"Tarefa '{tarefaID}' não encontrada.");

            await _tarefaHistoricoRepository.InserirOuAtualizarAsync(
                new(tarefa, TipoHistoricoEnum.Comentarios, comentario, _autenticacaoProvider.Usuario),
                cancellationToken);
        }
    }
}