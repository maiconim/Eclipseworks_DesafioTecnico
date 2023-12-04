using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Commons.Repository;

namespace DesafioTecnico.Business.Repositories
{
    internal interface ITarefaHistoricoRepository : IRepositoryBase<TarefaHistoricoEntity>
    {
        Task<IEnumerable<TarefaHistoricoEntity>> GetAllAsync(Guid tarefaId, CancellationToken cancellationToken);
    }
}