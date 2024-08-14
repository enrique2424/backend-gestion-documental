using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IPrestamoRepository: IGenericRepository<Prestamo>
    {
        public Task<IEnumerable<PrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<Prestamo> BuscarPorNumSecXcodPrestamo(string? cod_prestamo);
    }
}
