using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IDocumentacionDerivadaService : IGenericService<DocumentacionDerivada>
    {
        public Task<RespuestaListado<DocumentacionDerivadaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar,long nsec_area_destino);
    }
}
