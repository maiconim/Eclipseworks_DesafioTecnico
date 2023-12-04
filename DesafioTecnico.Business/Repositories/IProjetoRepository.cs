using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Commons.Repository;
using DesafioTecnico.Business.Services.Models;

namespace DesafioTecnico.Business.Repositories
{
    internal interface IProjetoRepository : IRepositoryBase<ProjetoEntity>
    {
        /// <summary>
        /// Lista os projetos do banco de dados
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns></returns>
        Task<IEnumerable<ProjetoEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Recupera do banco de dados um projeto pelo seu ID
        /// </summary>
        /// <param name="id">ID do projeto</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        /// <returns><see cref="ProjetoModel"/>Projeto ou nulo</returns>
        Task<ProjetoEntity?> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}