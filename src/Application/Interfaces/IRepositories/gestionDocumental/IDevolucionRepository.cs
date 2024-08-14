using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDevolucionRepository: IGenericRepository<Devolucion>
    {
        public Task<IEnumerable<DevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<Devolucion> TraerDevXNsecPrestamo(string nsec_prestamo);
    }
}
