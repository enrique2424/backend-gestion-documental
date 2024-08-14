using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IDevolucionService : IGenericService<Devolucion>
    {
        public Task<RespuestaListado<DevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<DevolucionMasterDto> TraerDevXNsecPrestamo(string? cod_prestamo);
    }
}
