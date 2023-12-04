using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Commons.Repository;

namespace DesafioTecnico.Business.Repositories
{
    internal interface ITarefaRepository : IRepositoryBase<TarefaEntity>
    {
        Task<IEnumerable<TarefaEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TarefaEntity>> GetAllAsync(Guid projetoID, CancellationToken cancellationToken);
        Task<TarefaEntity?> GetAsync(Guid id, CancellationToken cancellationToken);
    }
}