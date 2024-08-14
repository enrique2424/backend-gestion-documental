using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface ISeccionRepository: IGenericRepository<Seccion>
    {
        public Task<IEnumerable<SeccionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<SeccionAdjuntoDto>> BuscarListadoNsecDocumento(string? nsec_documento);
        public Task<IEnumerable<SeccionAdjuntoDto>> BuscarListadoNsecDocumentoObjeto(string? nsec_documento);
    }
}
