using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Enums;
using DesafioTecnico.Business.Helpers;
using DesafioTecnico.Business.Repositories;
using DesafioTecnico.Business.Services.Models;

namespace DesafioTecnico.Business.Services.Impl
{
    internal class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly ITarefaRepository _tarefaRepository;

        public ProjetoService(
            IProjetoRepository projetoRepository,
            ITarefaRepository tarefaRepository)
        {
            _projetoRepository = projetoRepository;
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Guid> InserirOuAtualizarAsync(ProjetoModel projetoModel, CancellationToken cancellationToken)
        {
            var entity = new ProjetoEntity(projetoModel.Descricao);
            ProtectedProperty.SetValue(entity, p => p.Id, projetoModel.Id);

            await _projetoRepository.InserirOuAtualizarAsync(entity, cancellationToken);

            return entity.Id;
        }

        public async Task ApagarAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _projetoRepository.GetAsync(id, cancellationToken)
                ?? throw new Exception($"Projeto '{id}' não localizado.");

            await CanDeleteAsync(id, cancellationToken);

            var tarefas = await _tarefaRepository.GetAllAsync(entity.Id, cancellationToken);
            foreach (var tarefa in tarefas)
                await _tarefaRepository.ApagarAsync(tarefa, cancellationToken);

            await _projetoRepository.ApagarAsync(entity, cancellationToken);
        }

        private async Task CanDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var tarefas = await _tarefaRepository.GetAllAsync(id, cancellationToken);
            var total = tarefas
                .Where(w => w.Situacao != TarefaSituacaoEnum.Fechada)
                .Count();
            if (total > 0)
                throw new Exception($"Há {total} tarefa(s) pendente(s). Conclua as tarefas ou remova-as antes de apagar o projeto.");
        }

        public async Task<IEnumerable<ProjetoModel>> ListarProjetosAsync(CancellationToken cancellationToken)
        {
            var result = await _projetoRepository.GetAllAsync(cancellationToken);
            return result
                .Select(s => new ProjetoModel()
                {
                    Id = s.Id,
                    Descricao = s.Descricao,
                    TotalTarefas = s.Tarefas.Count()
                })
                .ToList();
        }
    }
}