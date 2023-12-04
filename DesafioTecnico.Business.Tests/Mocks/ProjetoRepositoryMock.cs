using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Helpers;
using DesafioTecnico.Business.Repositories;
using Moq;

namespace DesafioTecnico.Business.Tests.Mocks
{
    internal class ProjetoRepositoryMock
    {
        private readonly IList<ProjetoEntity> _projects;
        private readonly Mock<IProjetoRepository> _mock;

        public IProjetoRepository Instance =>
            _mock.Object;

        public ProjetoRepositoryMock()
        {
            _projects = new List<ProjetoEntity>();
            _mock = new Mock<IProjetoRepository>();

            _mock
                .Setup(s => s.InserirOuAtualizarAsync(It.IsAny<ProjetoEntity>(), It.IsAny<CancellationToken>()))
                .Callback((ProjetoEntity entity, CancellationToken cancellation) =>
                {
                    if (_projects.Any(a => a.Id == entity.Id))
                    {
                        var index = _projects.IndexOf(_projects.First(w => w.Id == entity.Id));
                        _projects[index] = entity;
                        return;
                    }

                    ProtectedProperty.SetValue(entity, o => o.Id, Guid.NewGuid());
                    _projects.Add(entity);
                });

            _mock
                .Setup(s => s.ApagarAsync(It.IsAny<ProjetoEntity>(), It.IsAny<CancellationToken>()))
                .Callback((ProjetoEntity entity, CancellationToken CancellationToken) =>
                {
                    var reg = _projects.FirstOrDefault(w => w.Id == entity.Id)
                        ?? throw new Exception("Registro não encontrado.");
                    _projects.Remove(reg);
                });

            _mock
                .Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((CancellationToken CancellationToken) =>
                {
                    return _projects.ToList();
                });

            _mock
                .Setup(s => s.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken CancellationToken) =>
                {
                    return _projects.FirstOrDefault(w => w.Id == id);
                });
        }

        public void Inicializar()
        {
            var t =
                new List<Task>
                {
                    Instance.InserirOuAtualizarAsync(new ProjetoEntity("Projeto 1"), default),
                    Instance.InserirOuAtualizarAsync(new ProjetoEntity("Projeto 2"), default),
                    Instance.InserirOuAtualizarAsync(new ProjetoEntity("Projeto 3"), default)
                };
            Task.WaitAll(t.ToArray());
        }
    }
}