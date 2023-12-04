using System.ComponentModel;

namespace DesafioTecnico.Business.Commons
{
    public interface IAutenticacaoProvider
    {
        bool Autenticado { get; }
        string? Usuario { get; }

        void Autenticar(TipoUsuarioEnum usuario);
        void Desconectar();
    }

    public enum TipoUsuarioEnum
    {
        /// <summary>
        /// Usuário
        /// </summary>
        [Description("Usuário")]
        Usuario = 0,

        /// <summary>
        /// Gerência
        /// </summary>
        [Description("Gerência")]
        Gerencia
    }
}