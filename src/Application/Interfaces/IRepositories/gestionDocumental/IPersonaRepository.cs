using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IPersonaRepository: IGenericRepository<Persona>
    {
        public Task<IEnumerable<PersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
