using DesafioTecnico.Business.Enums;

namespace DesafioTecnico.Business.Commons.Entities
{
    internal class TarefaHistoricoEntity : EntityBase
    {
        public Guid TarefaId { get; protected set; } = default!;
        public TarefaEntity Tarefa { get; protected set; } = default!;
        public DateTime DataHora { get; protected set; } = DateTime.Now;
        public TipoHistoricoEnum TipoHistorico { get; protected set; } = TipoHistoricoEnum.Alteracao;
        public string Usuario { get; protected set; } = default!;
        public string Historico { get; protected set; } = default!;

        protected TarefaHistoricoEntity() { }
        public TarefaHistoricoEntity(TarefaEntity tarefa, TipoHistoricoEnum tipo, string historico, string usuario) : this()
        {
            if (string.IsNullOrWhiteSpace(historico))
                throw new Exception("O histórico deve ser informado.");
            if (string.IsNullOrWhiteSpace(usuario))
                throw new Exception("O usuário deve ser informado.");

            Tarefa = tarefa ?? throw new Exception("A tarefa não foi informada.");
            TarefaId = tarefa.Id;
            TipoHistorico = tipo;
            Historico = historico;
            Usuario = usuario;
        }

        
    }
}