namespace TiendaServicios.Mensajeria.Email.Modelo
{
    public class SendGridData
    {
        public string SendGridAPIKey { get; set; }
        public string EmailDestinatario { get; set; }
        public string NombreDestinatario { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}