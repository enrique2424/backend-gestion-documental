using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface ISgDocumentoIdentificacionRepository
    {
        public Task<IEnumerable<SgDocumentoIdentificacionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<SgDocumentoIdentificacion> BuscarPorNumSec(long  numSec);
        public Task<RespuestaDB> Guardar(SgDocumentoIdentificacion datos);
        public Task<RespuestaDB> Modificar(long numsec, SgDocumentoIdentificacion datos);
        public Task<RespuestaDB> delete(long numSec);
        
    }
}
