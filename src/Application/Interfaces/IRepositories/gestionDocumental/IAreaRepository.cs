using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IAreaRepository: IGenericRepository<Area>
    {
        public Task<IEnumerable<AreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<AreaDto>> BuscarListadoxUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, int nsecUsuario);
    }
}
