using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;
using SharpCompress.Archives;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IAdjuntoService : IGenericService<Adjunto>
    {
        public Task<RespuestaListado<AdjuntoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaDB> PostArchivo(Adjunto datos);

        public Task<RespuestaDB> PostArchivoMongo(PostArchivo datos);
        public Task<RespuestaDB> PutArchivoMongo(PostArchivo datos);
    }
}