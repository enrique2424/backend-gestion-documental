using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDetSeccionFiltroBusquedaRepository: IGenericRepository<DetSeccionFiltroBusqueda>
    {
        public Task<IEnumerable<DetSeccionFiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
