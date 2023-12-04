using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Helpers;
using DesafioTecnico.Business.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace DesafioTecnico.Business.Tests.Mocks
{
    internal class TarefaRepositoryMock
    {
        private readonly IList<TarefaEntity> _tarefas;
        private readonly Mock<ITarefaRepository> _mock;

        private readonly ITarefaHistoricoRepository _tarefaHistoricoRepository;

        public ITarefaRepository Instance =>
            _mock.Object;

        public TarefaRepositoryMock(ITarefaHistoricoRepository tarefaHistoricoRepository)
        {
            _tarefaHistoricoRepository= tarefaHistoricoRepository;

            _tarefas = new List<TarefaEntity>();
            _mock = new Mock<ITarefaRepository>();

            _mock
                .Setup(s => s.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken cancellationToken) =>
                {
                    var result=_tarefas
                        .Where(w => w.Id == id)
                        .FirstOrDefault();

                    if (result != null)
                        ProtectedProperty.SetValue(result, p => p.Historico, _tarefaHistoricoRepository.GetAllAsync(id, default).Result);

                    return result;
                });

            _mock
                .Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((CancellationToken c) =>
                {
                    var result= _tarefas.ToList();

                    foreach (var r in result)
                        ProtectedProperty.SetValue(r, p => p.Historico, _tarefaHistoricoRepository.GetAllAsync(r.Id, default).Result.AsEnumerable());

                    return result;
                });

            _mock
                .Setup(s => s.GetAllAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid projectId, CancellationToken CancellationToken) =>
                {
                    var result= _tarefas
                        .Where(w => w.ProjetoId == projectId)
                        .ToList();

                    foreach (var r in result)
                        ProtectedProperty.SetValue(r, p => p.Historico, _tarefaHistoricoRepository.GetAllAsync(r.Id, default).Result);

                    return result;
                });

            _mock
                .Setup(s => s.InserirOuAtualizarAsync(It.IsAny<TarefaEntity>(), It.IsAny<CancellationToken>()))
                .Callback((TarefaEntity entity, CancellationToken cancellation) =>
                {
                    if (_tarefas.Any(a => a.Id == entity.Id))
                    {
                        var index = _tarefas.IndexOf(_tarefas.First(f => f.Id == entity.Id));
                        _tarefas[index] = entity;
                        return;
                    }

                    ProtectedProperty.SetValue(entity, o => o.Id, Guid.NewGuid());
                    _tarefas.Add(entity);
                });

            _mock
                .Setup(s => s.ApagarAsync(It.IsAny<TarefaEntity>(), It.IsAny<CancellationToken>()))
                .Callback((TarefaEntity entity, CancellationToken CancellationToken) =>
                {
                    var reg = _tarefas.FirstOrDefault(w => w.Id == entity.Id)
                        ?? throw new Exception("Registro não encontrado.");
                    _tarefas.Remove(reg);
                });
        }

        public void Inicializar(ProjetoEntity projeto)
        {
            var t =
                new List<Task>
                {
                    Instance.InserirOuAtualizarAsync(new TarefaEntity(projeto, $"Tarefa 1 ({projeto.Descricao})", "Tarefa 1 Descrição", Enums.TarefaPrioridadeEnum.Alta, Enums.TarefaSituacaoEnum.EmAberto, "Usuário 1"), default),
                    Instance.InserirOuAtualizarAsync(new TarefaEntity(projeto, $"Tarefa 2 ({projeto.Descricao})", "Tarefa 2 Descrição", Enums.TarefaPrioridadeEnum.Media, Enums.TarefaSituacaoEnum.EmAberto,"Usuário 2"), default), 
                    Instance.InserirOuAtualizarAsync(new TarefaEntity(projeto, $"Tarefa 3 ({projeto.Descricao})", "Tarefa 3 Descrição", Enums.TarefaPrioridadeEnum.Baixa, Enums.TarefaSituacaoEnum.EmAberto,"Usuário 3"), default),
                    Instance.InserirOuAtualizarAsync(new TarefaEntity(projeto, $"Tarefa 4 ({projeto.Descricao})", "Tarefa 4 Descrição", Enums.TarefaPrioridadeEnum.Alta, Enums.TarefaSituacaoEnum.EmAberto,"Usuário 4"), default),
                };
            Task.WaitAll(t.ToArray());
        }
    }
}