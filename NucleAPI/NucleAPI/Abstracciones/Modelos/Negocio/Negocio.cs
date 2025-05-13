namespace Abstracciones.Modelos.Negocio
{
    public class NegocioBase
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
    }

    public class NegocioRequest : NegocioBase
    {
        public Guid fkRol { get; set; }
    }

    public class NegocioResponse : NegocioBase
    {
        public Guid idNegocio { get; set; }
        public string descripcionRol { get; set; } // Cambiado para coincidir con el SP
    }
}