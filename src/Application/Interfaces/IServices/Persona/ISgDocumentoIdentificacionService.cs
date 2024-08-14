using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface ISgDocumentoIdentificacionService 
    {
        public Task<RespuestaListado<SgDocumentoIdentificacionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<SgDocumentoIdentificacion> BuscarPorNumSec(string numSec);
        public Task<RespuestaDB> Guardar(SgDocumentoIdentificacion datos);
        
        public Task<RespuestaDB> delete(long numSec);
        
    }
    
}
