using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDetPrestamoRepository: IGenericRepository<DetPrestamo>
    {
        public Task<IEnumerable<DetPrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<traerDetallePorPrestamoDto>> TraerDetallePorPrestamo(int nsec_prestamo);

    }
}
