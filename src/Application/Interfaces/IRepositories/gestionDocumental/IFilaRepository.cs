using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IFilaRepository: IGenericRepository<Fila>
    {
        public Task<IEnumerable<FilaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<FilaDto>> TraerFilaXnec_estante(string? nsecEstante);
    }
}
