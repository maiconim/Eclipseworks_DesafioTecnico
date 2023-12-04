using DesafioTecnico.Business.Services.Models;

namespace DesafioTecnico.Business.Services
{
    public interface IProjetoService
    {
        /// <summary>
        /// Lista os projetos cadastrados
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns><see cref="IEnumerable{ProjetoInfoModel}"/>Lista dos projetos</returns>
        Task<IEnumerable<ProjetoModel>> ListarProjetosAsync(CancellationToken cancellationToken);

        Task<Guid> InserirOuAtualizarAsync(ProjetoModel projetoModel, CancellationToken cancellationToken);

        Task ApagarAsync(Guid id, CancellationToken cancellationToken);
    }
}