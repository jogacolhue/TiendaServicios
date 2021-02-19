using System;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.Comandos
{
    public abstract class Comando : Message
    {
        public DateTime Timestamp { get; protected set; }

        protected Comando()
        {
            Timestamp = DateTime.Now;
        }
    }
}