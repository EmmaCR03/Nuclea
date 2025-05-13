using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos.Seguridad
{
    public class UsuarioBase
    {
        [Required]
        public string NombreUsuario { get; set; }
        public string? PasswordHash { get; set; }
        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
    }

    public class Usuario : UsuarioBase
    {
        [Required]
        public string Password { get; set; }
        public string ConfirmarPassword { get; set; }
    }

    public class UsuarioResponse : UsuarioBase
    {
        public Guid Id { get; set; }
    }

    public class UsuarioRequest : UsuarioBase
    {
        public Guid Id { get; set; }

    }
}
