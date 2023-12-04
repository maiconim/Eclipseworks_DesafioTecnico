using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Commons.Repository;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Business.Repositories.Impl
{
    internal class TarefaRepository : RepositoryBase<TarefaEntity>, ITarefaRepository
    {
        public TarefaRepository(DesafioTecnicoContext context) : base(context) { }

        public async Task<IEnumerable<TarefaEntity>> GetAllAsync(CancellationToken cancellationToken) =>
            await DbSet
                .Include(s => s.Historico)
                .ToListAsync(cancellationToken);

        public async Task<IEnumerable<TarefaEntity>> GetAllAsync(Guid projetoID, CancellationToken cancellationToken) =>
            await DbSet
                .Where(w => w.ProjetoId == projetoID)
                .Include(s => s.Historico)
                .ToListAsync(cancellationToken);

        public async Task<TarefaEntity?> GetAsync(Guid id, CancellationToken cancellationToken) =>
            await DbSet
                .Include(s => s.Historico)
                .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }
}