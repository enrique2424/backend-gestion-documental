using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface ISgEstadoCivilRepository
    {
        public Task<IEnumerable<SgEstadoCivilDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<SgEstadoCivil> BuscarPorNumSec(long numSec);
    }
}
