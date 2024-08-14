using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IVolumenRepository: IGenericRepository<Volumen>
    {
        public Task<IEnumerable<VolumenDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<VolumenDto>> BuscarListadoNsecDocumento(string? nsecTomo);
        
    }
}
