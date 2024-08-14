using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IRepositories.GestionDocumental
{
    public interface IAdjuntoRepository: IGenericRepository<Adjunto>
    {
        public Task<IEnumerable<AdjuntoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);

        public Task<RespuestaDB> PostArchivo(Adjunto datos);
        public Task<RespuestaDB> PutArchivo(Adjunto datos);
        public Task<RespuestaDB> PostArchivoMongo(PostArchivo datos);
    }
}
