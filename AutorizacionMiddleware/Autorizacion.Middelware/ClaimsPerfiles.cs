using Autorizacion.Abstracciones.BW;

using Autorizacion.Abstracciones.Modelos;

using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;

using System.Security.Claims;


namespace Autorizacion.Middleware

{

    public class ClaimsPerfiles

    {

        private readonly RequestDelegate _next;

        private readonly IConfiguration _configuration;

        private IAutorizacionBW _autorizacionBW;

        public ClaimsPerfiles(RequestDelegate next, IConfiguration configuration)

        {

            _next = next;

            _configuration = configuration;

        }

        public async Task InvokeAsync(HttpContext httpContext, IAutorizacionBW autorizacionBW)

        {

            _autorizacionBW = autorizacionBW;

            ClaimsIdentity appIdenty = await verificarAutorizacion(httpContext);

            httpContext.User.AddIdentity(appIdenty);

            await _next(httpContext);

        }

        private async Task<ClaimsIdentity> verificarAutorizacion(HttpContext httpContext)

        {

            var claims = new List<Claim>();

            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)

            {

                await ObtenerUsuario(httpContext, claims);

                await ObtenerPerfiles(httpContext, claims);

            }

            var appIdentity = new ClaimsIdentity(claims);

            return appIdentity;

        }

        private async Task ObtenerPerfiles(HttpContext httpContext, List<Claim> claims)

        {

            var perfiles = await ObtenerInformacionPerfiles(httpContext);

            if (perfiles != null && perfiles.Any())

            {

                foreach (var perfil in perfiles)

                {

                    claims.Add(new Claim(ClaimTypes.Role, perfil.Id.ToString()));

                }

            }

        }

        private async Task<IEnumerable<Perfil>> ObtenerInformacionPerfiles(HttpContext httpContext)

        {

            return await _autorizacionBW.ObtenerPerfilesXUsuario(new Abstracciones.Modelos.Usuario
            {
                NombreUsuario = httpContext.User.Claims.Where(c => c.Type == "usuario").FirstOrDefault

              ().Value
            });

        }

        private async Task ObtenerUsuario(HttpContext httpContext, List<Claim> claims)

        {

            var usuario = await ObtenerInformacionUsuario(httpContext);

            if (usuario is not null && !string.IsNullOrEmpty(usuario.Id.ToString()) && !string.IsNullOrEmpty(usuario.NombreUsuario.ToString()) && !string.IsNullOrEmpty

                (usuario.CorreoElectronico.ToString()))

            {

                claims.Add(new Claim(ClaimTypes.Email, usuario.CorreoElectronico));

                claims.Add(new Claim(ClaimTypes.Name, usuario.NombreUsuario));

                claims.Add(new Claim("IdUsuario", usuario.Id.ToString()));

            }

        }

        private async Task<Usuario> ObtenerInformacionUsuario(HttpContext httpContext)

        {

            return await _autorizacionBW.ObtenerUsuario(new Abstracciones.Modelos.Usuario
            {
                NombreUsuario = httpContext.User.Claims.Where(c => c.Type == "usuario").FirstOrDefault

                ().Value
            });

        }

    }

}

