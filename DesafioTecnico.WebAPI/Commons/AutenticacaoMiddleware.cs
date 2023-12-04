using DesafioTecnico.Business.Commons;

namespace DesafioTecnico.WebAPI.Commons
{
    public class AutenticacaoMiddleware
    {
        private readonly RequestDelegate _next;

        public AutenticacaoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAutenticacaoProvider autenticacaoProvider)
        {
            var user = context.Request.Headers["x-user"].FirstOrDefault();
            if (user == null)
            {
                autenticacaoProvider.Desconectar();
                await _next(context);
                return;
            }

            autenticacaoProvider.Autenticar((TipoUsuarioEnum)Convert.ToInt32(user));
            await _next(context);
        }

    }
}