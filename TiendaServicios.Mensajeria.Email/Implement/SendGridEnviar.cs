using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.Interface;
using TiendaServicios.Mensajeria.Email.Modelo;

namespace TiendaServicios.Mensajeria.Email.Implement
{
    public class SendGridEnviar : ISendGridEnviar
    {
        public async Task<(bool resultado, string mensajeError)> EnviarEmail(SendGridData data)
        {
            try
            {
                var sendGridCliente = new SendGridClient(data.SendGridAPIKey);
                var destinatario = new EmailAddress(data.EmailDestinatario, data.NombreDestinatario);
                var tituloEmail = data.Titulo;
                var sender = new EmailAddress("jogabch32@gmail.com", "Jose Colque");
                var contenidoMensaje = data.Contenido;

                var objMensaje = MailHelper.CreateSingleEmail(sender, destinatario, tituloEmail, contenidoMensaje, contenidoMensaje);

                var resultado = await sendGridCliente.SendEmailAsync(objMensaje);

                if (resultado.IsSuccessStatusCode)
                    return (true, null);
                else
                    throw new Exception("Fallo en el envío del mensaje.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}