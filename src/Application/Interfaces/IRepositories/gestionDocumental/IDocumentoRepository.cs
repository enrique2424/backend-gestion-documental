using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDocumentoRepository: IGenericRepository<Documento>
    {
        public Task<IEnumerable<DocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<DocumentoConEstadoDto>> BuscarListadoxArea(string? valor, string? parametro, int numeroPagina, int cantidadMostrar,string nsec_area);
        public Task<IEnumerable<BusquedaDocumentoDto>> BuscarDocumento(BusquedaDocumento parametros);
        public Task<IEnumerable<DetalleDocumentoSeccionDto>> BuscarDetalleSeccion(long nsec_documento);
        //public Task<DetalleDocumentoDto> TraerdetalleDocumento(string nsecOrganigrama);
    }
}
