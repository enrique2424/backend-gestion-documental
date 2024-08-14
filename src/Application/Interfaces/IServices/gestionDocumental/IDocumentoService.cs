using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IDocumentoService : IGenericService<Documento>
    {
        public Task<RespuestaListado<DocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaListado<DocumentoConEstadoDto>> BuscarListadoxArea(string? valor, string? parametro, int numeroPagina, int cantidadMostrar,string nsec_area);
        public Task<DetalleDocumentoDto> TraerdetalleDocumento(string nsec_organigrama);
        public Task<RespuestaListado<BusquedaDocumentoDto>> BuscarDocumento(BusquedaDocumento parametros);
        public Task<IEnumerable<DetalleDocumentoSeccionDto>> BuscarDetalleSeccion(long nsec_documento);

    }
}
