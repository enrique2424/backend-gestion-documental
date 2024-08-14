using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IEstadoPrestamoService : IGenericService<EstadoPrestamo>
    {
        public Task<RespuestaListado<EstadoPrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
