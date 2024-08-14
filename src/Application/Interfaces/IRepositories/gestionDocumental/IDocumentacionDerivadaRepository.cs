using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IDocumentacionDerivadaRepository: IGenericRepository<DocumentacionDerivada>
    {
        public Task<IEnumerable<DocumentacionDerivadaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, long nsec_area_destino);
    }
}
