using DesafioTecnico.Business.Services;
using DesafioTecnico.Business.Services.Impl;
using DesafioTecnico.Business.Services.Models;
using DesafioTecnico.Business.Tests.Mocks;

namespace DesafioTecnico.Business.Tests.Unit.Services
{
    [TestClass]
    public class ProjetoServiceTests
    {
        private readonly ProjetoRepositoryMock _projetoRepositoryMock;
        private readonly TarefaRepositoryMock _tarefaRepositoryMock;
        private readonly TarefaHistoricoRepositoryMock _tarefaHistoricoRepositoryMock;
        private readonly IProjetoService _projetoService;

        public ProjetoServiceTests()
        {
            _projetoRepositoryMock = new ProjetoRepositoryMock();
            _projetoRepositoryMock.Inicializar();

            _tarefaHistoricoRepositoryMock = new TarefaHistoricoRepositoryMock();

            _tarefaRepositoryMock = new TarefaRepositoryMock(_tarefaHistoricoRepositoryMock.Instance);
            foreach (var projeto in _projetoRepositoryMock.Instance.GetAllAsync(default).Result)
                _tarefaRepositoryMock.Inicializar(projeto);

            _projetoService = new ProjetoService(_projetoRepositoryMock.Instance, _tarefaRepositoryMock.Instance);
        }

        [TestMethod]
        public async Task DeveListarOsProjetos()
        {
            var resultado = await _projetoService.ListarProjetosAsync(default);
            Assert.AreEqual(3, resultado.Count());
        }

        [TestMethod]
        public async Task DeveIncluirProjeto()
        {
            var model =
                new ProjetoModel()
                {
                    Descricao = "Teste"
                };
            var id = await _projetoService.InserirOuAtualizarAsync(model, default);
            Assert.AreNotEqual(Guid.Empty, id);
        }

        [TestMethod]
        public async Task DeveAlterarProjeto()
        {
            var projetos = await _projetoRepositoryMock.Instance.GetAllAsync(default);
            var projetoRef = projetos.First();

            var model =
                new ProjetoModel()
                {
                    Id = projetoRef.Id,
                    Descricao = "Teste XX"
                };
            await _projetoService.InserirOuAtualizarAsync(model, default);

            projetoRef = await _projetoRepositoryMock.Instance.GetAsync(projetoRef.Id, default);
            Assert.IsNotNull(projetoRef);
            Assert.AreEqual("Teste XX", projetoRef.Descricao);
        }

        [TestMethod]
        public async Task DeveApagarProjeto()
        {
            var model =
                new ProjetoModel()
                {
                    Descricao = "Teste"
                };
            var id = await _projetoService.InserirOuAtualizarAsync(model, default);
            var projeto = await _projetoRepositoryMock.Instance.GetAsync(id, default);
            Assert.IsNotNull(projeto);

            await _projetoService.ApagarAsync(id, default);
            projeto = await _projetoRepositoryMock.Instance.GetAsync(id, default);
            Assert.IsNull(projeto);
        }

        [TestMethod]
        public async Task NaoDeveApagarProjetoComTarefasPendentes()
        {
            var tarefas= await _tarefaRepositoryMock.Instance.GetAllAsync(default);
            var projetoId = tarefas.First().ProjetoId;

            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await _projetoService.ApagarAsync(projetoId, default);
            });            
        }
    }
}