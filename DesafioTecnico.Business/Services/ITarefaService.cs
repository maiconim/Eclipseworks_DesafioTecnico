using DesafioTecnico.Business.Services.Models;

namespace DesafioTecnico.Business.Services
{
    public interface ITarefaService
    {
        Task<Guid> InserirOuAlterarAsync(TarefaModel tarefa, CancellationToken cancellationToken);
        Task ApagarAsync(Guid tarefaID, CancellationToken cancellationToken);
        Task AdicionarComentarioAsync(Guid tarefaID, string comentario, CancellationToken cancellationToken);
    }
}