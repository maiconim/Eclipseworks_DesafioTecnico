using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Services;
using DesafioTecnico.Business.Services.Impl;
using DesafioTecnico.Business.Services.Models;
using DesafioTecnico.Business.Tests.Commons;
using DesafioTecnico.Business.Tests.Mocks;

namespace DesafioTecnico.Business.Tests.Unit.Services
{
    [TestClass]
    public class TarefaServiceTests
    {
        private readonly ProjetoRepositoryMock _projetoRepositoryMock;
        private readonly TarefaRepositoryMock _tarefaRepositoryMock;
        private readonly TarefaHistoricoRepositoryMock _tarefaHistoricoRepositoryMock;
        private readonly ITarefaService _tarefaService;
        private readonly IAutenticacaoProvider _autenticacaoProvider;

        public TarefaServiceTests()
        {
            _autenticacaoProvider = new AutenticacaoProvider();
            _autenticacaoProvider.Autenticar(TipoUsuarioEnum.Usuario);

            _projetoRepositoryMock = new ProjetoRepositoryMock();
            _projetoRepositoryMock.Inicializar();

            _tarefaHistoricoRepositoryMock = new TarefaHistoricoRepositoryMock();

            _tarefaRepositoryMock = new TarefaRepositoryMock(_tarefaHistoricoRepositoryMock.Instance);
            foreach (var projeto in _projetoRepositoryMock.Instance.GetAllAsync(default).Result)
                _tarefaRepositoryMock.Inicializar(projeto);


            _tarefaService = new TarefaService(_projetoRepositoryMock.Instance, _tarefaRepositoryMock.Instance, _tarefaHistoricoRepositoryMock.Instance, _autenticacaoProvider);
        }

        [TestMethod]
        public async Task DeveListarAsTarefasDeTodosOsProjetos()
        {
            foreach (var projeto in await _projetoRepositoryMock.Instance.GetAllAsync(default))
            {
                var resultado = await _tarefaRepositoryMock.Instance.GetAllAsync(projeto.Id, default);
                Assert.AreEqual(4, resultado.Count());
            }
        }

        [TestMethod]
        public async Task DeveListarTodasAsTarefas()
        {
            var resultado = await _tarefaRepositoryMock.Instance.GetAllAsync(default);
            Assert.AreEqual(12, resultado.Count());
        }

        [TestMethod]
        public async Task DeveBuscarUmaTarefa()
        {
            var tarefas = await _tarefaRepositoryMock.Instance.GetAllAsync(default);
            var tarefaRef = tarefas.First();

            var resultado = await _tarefaRepositoryMock.Instance.GetAsync(tarefaRef.Id, default);
            Assert.IsNotNull(resultado);
            Assert.AreEqual(tarefaRef.Id, resultado.Id);
        }

        [TestMethod]
        public async Task NaoDeveGerarHistoricoCriacaoTarefa()
        {
            var projetos = await _projetoRepositoryMock.Instance.GetAllAsync(default);
            var model =
                new TarefaModel()
                {
                    Descricao = "Tarefa criada",
                    Projeto = new ProjetoModel() { Id = projetos.First().Id },
                    Titulo = "Tarefa criada",
                    Prioridade = Enums.TarefaPrioridadeEnum.Baixa
                };

            var id = await _tarefaService.InserirOuAlterarAsync(model, default);
            var tarefa = await _tarefaRepositoryMock.Instance.GetAsync(id, default);

            Assert.IsNotNull(tarefa);

            var qtde = tarefa.Historico.Count();
            Assert.AreEqual(0, qtde);
        }

        [TestMethod]
        public async Task DeveGerarHistoricoAtualizacaoTarefa()
        {
            var tarefas = await _tarefaRepositoryMock.Instance.GetAllAsync(default);
            var tarefaRef = tarefas.First();

            var model =
                new TarefaModel()
                {
                    Id = tarefaRef.Id,
                    Descricao = "Tarefa modificada",
                    Titulo = "Tarefa Modificaca",
                    Situacao = Enums.TarefaSituacaoEnum.ComImpedimento
                };

            await _tarefaService.InserirOuAlterarAsync(model, default);

            tarefaRef = await _tarefaRepositoryMock.Instance.GetAsync(tarefaRef.Id, default);
            Assert.IsNotNull(tarefaRef);
            Assert.AreEqual(1, tarefaRef.Historico.Count());

            var historico = await _tarefaHistoricoRepositoryMock.Instance.GetAllAsync(tarefaRef.Id, default);
            Assert.AreNotEqual(0, historico.Count());
            Assert.AreEqual(tarefaRef.Historico.Count(), historico.Count());
        }

        [TestMethod]
        public async Task DeveApagarUmaTarefa()
        {
            var tarefas = await _tarefaRepositoryMock.Instance.GetAllAsync(default);
            var total = tarefas.Count();

            await _tarefaService.ApagarAsync(tarefas.First().Id, default);

            tarefas = await _tarefaRepositoryMock.Instance.GetAllAsync(default);
            Assert.IsTrue(total > tarefas.Count());
        }
    }
}