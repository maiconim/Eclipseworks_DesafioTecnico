using DesafioTecnico.Business.Enums;

namespace DesafioTecnico.Business.Commons.Entities
{
    internal class TarefaEntity : EntityBase
    {
        public Guid ProjetoId { get; protected set; } = default!;
        public ProjetoEntity Projeto { get; protected set; } = default!;
        public string Titulo { get; protected set; } = default!;
        public string Descricao { get; protected set; } = default!;
        public TarefaPrioridadeEnum Prioridade { get; protected set; } = TarefaPrioridadeEnum.Baixa;
        public TarefaSituacaoEnum Situacao { get; protected set; } = TarefaSituacaoEnum.EmAberto;
        public DateTime Criacao { get; protected set; } = DateTime.Now;
        public string UsuarioCriacao { get; protected set; } = default!;
        public string? UsuarioFechamento { get; protected set; }
        public DateTime? DataFechamento { get; protected set; }

        public ICollection<TarefaHistoricoEntity> Historico { get; protected set; } = new List<TarefaHistoricoEntity>();

        protected TarefaEntity() { }
        public TarefaEntity(ProjetoEntity projeto, string titulo, string descricao, TarefaPrioridadeEnum prioridade, TarefaSituacaoEnum situacao, string? usuarioCriacao) : this()
        {
            if (string.IsNullOrWhiteSpace(usuarioCriacao)) throw new Exception("Usuário não informado.");

            Projeto = projeto ?? throw new Exception("Projeto não informado.");
            ProjetoId = projeto.Id;
            Prioridade = prioridade;
            UsuarioCriacao = usuarioCriacao;

            AlterarTitulo(titulo);
            AlterarDescricao(descricao);
            AlterarSituacao(situacao, situacao == TarefaSituacaoEnum.Fechada ? usuarioCriacao : null);
        }

        public TarefaEntity AlterarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentNullException(nameof(titulo));
            if (string.Equals(Titulo, titulo)) return this;
            Changes[nameof(Titulo)] = new ChangesInfo(Titulo, titulo);
            Titulo = titulo;
            return this;
        }

        public TarefaEntity AlterarSituacao(TarefaSituacaoEnum situacao, string? usuarioFechamento)
        {
            if (Situacao == situacao) return this;
            if (situacao == TarefaSituacaoEnum.Fechada && string.IsNullOrWhiteSpace(usuarioFechamento))
                throw new Exception("Usuário do fechamento da tarefa deve ser informado.");

            Changes[nameof(Situacao)] = new ChangesInfo(Situacao, situacao);
            Situacao = situacao;

            if (situacao == TarefaSituacaoEnum.Fechada)
            {
                Changes[nameof(UsuarioFechamento)] = new ChangesInfo(UsuarioFechamento, usuarioFechamento);
                UsuarioFechamento = usuarioFechamento;
                DataFechamento = DateTime.Now;
            }
            return this;
        }

        public TarefaEntity AlterarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentNullException(nameof(descricao));
            if (string.Equals(Descricao, descricao)) return this;
            Changes[nameof(Descricao)] = new ChangesInfo(Descricao, descricao);
            Descricao = descricao;
            return this;
        }
    }
}