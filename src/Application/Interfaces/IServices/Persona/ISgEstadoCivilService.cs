using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface ISgEstadoCivilService 
    {
        public Task<RespuestaListado<SgEstadoCivilDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<SgEstadoCivil> BuscarPorNumSec(string numSec);
        public Task<RespuestaDB> Guardar(SgEstadoCivil datos);
        public Task<RespuestaDB> Modificar(long numsec, SgEstadoCivil datos);
        public Task<RespuestaDB> Eliminar(long numSec);
    }
}
