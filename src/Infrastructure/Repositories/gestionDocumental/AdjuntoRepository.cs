using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using Domain.Enums;
using Domain.Models.Data;
using SharpCompress.Archives;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class AdjuntoRepository : GenericRepository<Adjunto>, IAdjuntoRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AdjuntoRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<AdjuntoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<AdjuntoDto>? arrayDatos = new AdjuntoDto[] { };
                string nombreFuncion = "sp_listado_adjunto";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<AdjuntoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespuestaDB> PostArchivo(Adjunto datos)
        {
            try
            {
                RespuestaDB respuesta = new RespuestaDB();
                string nombreFuncion = $"sp_abm_adjunto";

                respuesta = await this._applicationDbContext.AbmObjeto<Adjunto>(nombreFuncion, AbmAccion.GUARDAR, datos);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<RespuestaDB> PostArchivoMongo(PostArchivo datos)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDB> PutArchivo(Adjunto datos)
        {
            try
            {
                RespuestaDB respuesta = new RespuestaDB();
                string nombreFuncion = $"sp_abm_adjunto";

                respuesta = await this._applicationDbContext.AbmObjeto<Adjunto>(nombreFuncion, AbmAccion.MODIFICAR, datos);

                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

