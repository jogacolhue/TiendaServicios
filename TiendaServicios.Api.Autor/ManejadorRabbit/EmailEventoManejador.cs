using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.Interface;
using TiendaServicios.Mensajeria.Email.Modelo;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;
        private readonly ISendGridEnviar _sendGrid;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, ISendGridEnviar sendGrid,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _logger = logger;
            _sendGrid = sendGrid;
            _configuration = configuration;
        }

        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation($"Este es el valor que consumo desde rabbitMQ {@event.Titulo}");

            var data = new SendGridData()
            {
                Contenido = @event.Contenido,
                EmailDestinatario = @event.Destinatario,
                NombreDestinatario = @event.Destinatario,
                SendGridAPIKey = _configuration["SendGrid:APIKey"],
                Titulo = @event.Titulo
            };

            var resultado = await _sendGrid.EnviarEmail(data);

            if (resultado.resultado)
            {
                await Task.CompletedTask;
                return;
            }
        }
    }
}