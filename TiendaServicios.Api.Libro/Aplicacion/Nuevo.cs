using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IRabbitEventBus _eventBus;

            public Manejador(ContextoLibreria contexto, IRabbitEventBus eventBus)
            {
                _contexto = contexto;
                _eventBus = eventBus;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial()
                {
                    AutorLibro = request.AutorLibro,
                    FechaPublicacion = request.FechaPublicacion,
                    Titulo = request.Titulo
                };

                _contexto.LibreriaMaterial.Add(libro);

                var valor = await _contexto.SaveChangesAsync();

                if (valor > 0)
                {
                    _eventBus.Publish(new EmailEventoQueue("jogabc_h_31@hotmail.com", "Titulo", "Contenido ejemplo"));
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Libro.");
            }
        }
    }
}