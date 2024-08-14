using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDetAdjuntoTablaRepository: IGenericRepository<DetAdjuntoTabla>
    {
        public Task<IEnumerable<DetAdjuntoTablaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<DetAdjuntoTablaDto>> BuscarArchivos(int nsec_seccion);
    }
}
