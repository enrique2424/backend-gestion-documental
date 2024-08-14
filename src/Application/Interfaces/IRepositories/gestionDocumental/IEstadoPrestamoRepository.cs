using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IEstadoPrestamoRepository: IGenericRepository<EstadoPrestamo>
    {
        public Task<IEnumerable<EstadoPrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
