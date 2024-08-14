using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface ITomoService : IGenericService<Tomo>
    {
        public Task<RespuestaListado<TomoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
