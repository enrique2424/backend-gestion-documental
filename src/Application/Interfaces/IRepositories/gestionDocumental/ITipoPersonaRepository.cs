using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface ITipoPersonaRepository: IGenericRepository<TipoPersona>
    {
        public Task<IEnumerable<TipoPersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
