using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IPersonaService : IGenericService<Persona>
    {
        public Task<RespuestaListado<PersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        Task Delete(RespuestaDB persona);
    }
}
