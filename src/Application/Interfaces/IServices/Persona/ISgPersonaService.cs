using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface ISgPersonaService 
    {
        public Task<RespuestaListado<SgPersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<SgPersona> BuscarPorNumSec(long numSec);
        public Task<RespuestaDB> Guardar(SgPersona datos);
        public Task<RespuestaDB> Modificar(long numsec,SgPersona datos);
        public Task<RespuestaDB> Eliminar (long numsec);
    }
}
