using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IDetAdjuntoTablaService : IGenericService<DetAdjuntoTabla>
    {
        public Task<RespuestaListado<DetAdjuntoTablaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<DetAdjuntoTablaDto>> BuscarArchivos(int nsec_seccion);
    }
}
