namespace DesafioTecnico.Business.Services.Models
{
    public class ProjetoModel
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Descricao { get; set; } = default!;
        public int TotalTarefas { get; set; } = 0;
    }
}