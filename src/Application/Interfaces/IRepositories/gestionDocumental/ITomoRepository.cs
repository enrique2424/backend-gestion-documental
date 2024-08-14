using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface ITomoRepository: IGenericRepository<Tomo>
    {
        public Task<IEnumerable<TomoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<TomoDto>> BuscarListadoNsecDocumento(string? nsecDocumento);

    }
}
