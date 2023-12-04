namespace DesafioTecnico.Business.Commons.Repository
{
    internal interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task InserirOuAtualizarAsync(TEntity entity, CancellationToken cancellationToken);
        Task ApagarAsync(TEntity entity, CancellationToken cancellationToken);
    }
}