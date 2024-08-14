using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IDetDevolucionService : IGenericService<DetDevolucion>
    {
        public Task<RespuestaListado<DetDevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<DetDevolucion> TraerDetDevoXPrestamoDocumento(string? nsec_devolucion, string? nsec_documento);
    }
}
