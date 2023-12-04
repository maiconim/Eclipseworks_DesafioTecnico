using DesafioTecnico.Business.Commons;

namespace DesafioTecnico.Business.Tests.Commons
{
    internal class AutenticacaoProvider : IAutenticacaoProvider
    {
        public bool Autenticado { get; private set; }

        public string? Usuario { get; private set; }

        public void Autenticar(TipoUsuarioEnum usuario)
        {
            Usuario = usuario == TipoUsuarioEnum.Usuario ? "Usuário" : "Gerente";
            Autenticado = true;
        }

        public void Desconectar()
        {
            Autenticado = false;
            Usuario = null;
        }
    }
}