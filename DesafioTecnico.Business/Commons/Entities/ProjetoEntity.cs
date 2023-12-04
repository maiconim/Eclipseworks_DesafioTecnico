namespace DesafioTecnico.Business.Commons.Entities
{
    internal class ProjetoEntity : EntityBase
    {
        public string Descricao { get; protected set; } = default!;
        public IList<TarefaEntity> Tarefas { get; protected set; } = new List<TarefaEntity>();

        protected ProjetoEntity() { }
        public ProjetoEntity(string descricao) : this()
        {
            AlterarDescricao(descricao);
        }

        public ProjetoEntity AlterarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new Exception("A descrição do projeto deve ser informada.");
            Descricao = descricao;
            return this;
        }
    }
}