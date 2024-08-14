using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface ISgPersonaRepository
    {
        public Task<IEnumerable<SgPersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaDB> Guardar(SgPersona datos);
        public Task<RespuestaDB> Eliminar(long numSec);
    }
}
