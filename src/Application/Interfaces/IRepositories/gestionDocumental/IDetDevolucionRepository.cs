using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDetDevolucionRepository: IGenericRepository<DetDevolucion>
    {
        public Task<IEnumerable<DetDevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<DetDevolucion> TraerDetDevoXPrestamoDocumento(string? nsec_devolucion, string? nsec_documento);
    }
}
