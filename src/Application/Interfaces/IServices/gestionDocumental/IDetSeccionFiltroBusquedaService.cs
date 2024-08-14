using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IDetSeccionFiltroBusquedaService : IGenericService<DetSeccionFiltroBusqueda>
    {
        public Task<RespuestaListado<DetSeccionFiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        
    }
}
