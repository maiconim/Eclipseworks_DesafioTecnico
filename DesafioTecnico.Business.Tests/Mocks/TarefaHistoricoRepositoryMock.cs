using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Repositories;
using Moq;

namespace DesafioTecnico.Business.Tests.Mocks
{
    internal class TarefaHistoricoRepositoryMock
    {
        private readonly IList<TarefaHistoricoEntity> _historicos;
        private readonly Mock<ITarefaHistoricoRepository> _mock;

        public ITarefaHistoricoRepository Instance =>
            _mock.Object;

        public TarefaHistoricoRepositoryMock()
        {
            _historicos = new List<TarefaHistoricoEntity>();
            _mock = new Mock<ITarefaHistoricoRepository>();

            _mock
                .Setup(s => s.GetAllAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid tarefaId, CancellationToken CancellationToken) =>
                {
                    return _historicos
                        .Where(w => w.TarefaId == tarefaId)
                        .ToList();
                });

            _mock
                .Setup(s => s.InserirOuAtualizarAsync(It.IsAny<TarefaHistoricoEntity>(), It.IsAny<CancellationToken>()))
                .Callback((TarefaHistoricoEntity entity, CancellationToken cancellation) =>
                {
                    var reg = _historicos.FirstOrDefault(f => f.Id == entity.Id);
                    if (reg == null)
                    {
                        _historicos.Add(entity);
                        return;
                    }

                    _historicos[_historicos.IndexOf(reg)] = entity;
                });

            _mock
                .Setup(s => s.ApagarAsync(It.IsAny<TarefaHistoricoEntity>(), It.IsAny<CancellationToken>()))
                .Callback((TarefaHistoricoEntity entity, CancellationToken CancellationToken) =>
                {
                    var reg = _historicos.FirstOrDefault(f => f.Id == entity.Id)
                        ?? throw new Exception("Registro não encontrado.");
                    _historicos.Remove(reg);
                });
        }
    }
}