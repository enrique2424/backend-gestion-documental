using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IAreaService : IGenericService<Area>
    {
        public Task<RespuestaListado<AreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);

        public Task<RespuestaListado<AreaDto>> BuscarListadoxUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar,int nsecUsuario);
    }
}
