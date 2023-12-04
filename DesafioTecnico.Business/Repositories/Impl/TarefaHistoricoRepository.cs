using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Commons.Repository;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Business.Repositories.Impl
{
    internal class TarefaHistoricoRepository : RepositoryBase<TarefaHistoricoEntity>, ITarefaHistoricoRepository
    {
        public TarefaHistoricoRepository(DesafioTecnicoContext context) : base(context) { }

        public async Task<IEnumerable<TarefaHistoricoEntity>> GetAllAsync(Guid tarefaId, CancellationToken cancellationToken) =>
            await DbSet
                .Where(w => w.TarefaId == tarefaId)
                .ToListAsync(cancellationToken);

    }
}