using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class Ejecuta : IRequest<LibreriaMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, LibreriaMaterialDto>
        {
            private ContextoLibreria _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoLibreria contextoAutor, IMapper mapper)
            {
                _contexto = contextoAutor;
                _mapper = mapper;
            }

            public async Task<LibreriaMaterialDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync() ??
                   throw new Exception("No se encontró el libro.");

                return _mapper.Map<LibreriaMaterial, LibreriaMaterialDto>(libro);
            }
        }
    }
}