using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IFilaService : IGenericService<Fila>
    {
        public Task<RespuestaListado<FilaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaListado<FilaDto>> TraerFilaXnec_estante(string nsec_estante);
    }
}
