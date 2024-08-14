using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IFiltroBusquedaRepository: IGenericRepository<FiltroBusqueda>
    {
        public Task<IEnumerable<FiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<FiltroBusquedaDto>> TraerListaFiltro(int nsec_tipo_documento);
        public Task<IEnumerable<FiltroBusqueda>> TraerFiltrosTipoDocumento(int nsec_tipo_documento);
    }

}
