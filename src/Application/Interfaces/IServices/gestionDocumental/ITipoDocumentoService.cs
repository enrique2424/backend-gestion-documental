using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface ITipoDocumentoService : IGenericService<TipoDocumento>
    {
        public Task<RespuestaListado<TipoDocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<TipoDocumentoMasterDto> TraerDocumentoMaster(int nsec_tipo_documento);
    }
}
