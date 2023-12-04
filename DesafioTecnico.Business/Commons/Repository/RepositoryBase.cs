using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Business.Commons.Repository
{
    internal abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DesafioTecnicoContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(DesafioTecnicoContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task ApagarAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task InserirOuAtualizarAsync(TEntity entity, CancellationToken cancellationToken)
        {
            if (entity.Id == Guid.Empty)
                await DbSet.AddAsync(entity, cancellationToken);
            else
                DbSet.Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}