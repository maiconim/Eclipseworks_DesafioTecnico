using DesafioTecnico.Business.Commons;
using DesafioTecnico.Business.Commons.Entities;
using DesafioTecnico.Business.Commons.Repository;
using DesafioTecnico.Business.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Business.Repositories.Impl
{
    internal class ProjetoRepository : RepositoryBase<ProjetoEntity>, IProjetoRepository
    {
        private readonly IMediator _mediator;

        public ProjetoRepository(
            DesafioTecnicoContext context,
            IMediator mediator) : base(context)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProjetoEntity>> GetAllAsync(CancellationToken cancellationToken) =>
            await DbSet.ToListAsync(cancellationToken);

        public Task<ProjetoEntity?> GetAsync(Guid id, CancellationToken cancellationToken) =>
            DbSet.FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

        public override async Task ApagarAsync(ProjetoEntity entity, CancellationToken cancellationToken)
        {
            await CanDeleteAsync(entity.Id, cancellationToken);
            await base.ApagarAsync(entity, cancellationToken);
        }

        private async Task CanDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var tarefas = await Context.Set<TarefaEntity>()
                .Where(w => w.Situacao != TarefaSituacaoEnum.Fechada)
                .CountAsync(cancellationToken);

            if (tarefas > 0)
                throw new Exception($"Há {tarefas} tarefa(s) pendente(s). Conclua as tarefas ou remova-as antes de apagar o projeto.");
        }
    }
}