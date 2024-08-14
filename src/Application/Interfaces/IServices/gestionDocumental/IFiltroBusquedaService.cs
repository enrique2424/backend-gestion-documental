using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IFiltroBusquedaService : IGenericService<FiltroBusqueda>
    {
        public Task<RespuestaListado<FiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaListado<FiltroBusquedaDto>> TraerListaFiltro(int nsec_tipo_documento);
       
    }
}
