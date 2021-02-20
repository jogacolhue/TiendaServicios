using System.Threading.Tasks;
using TiendaServicios.Mensajeria.Email.Modelo;

namespace TiendaServicios.Mensajeria.Email.Interface
{
    public interface ISendGridEnviar
    {
        Task<(bool resultado, string mensajeError)> EnviarEmail(SendGridData data);
    }
}