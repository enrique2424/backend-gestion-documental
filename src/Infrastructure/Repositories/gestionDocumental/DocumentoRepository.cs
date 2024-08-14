using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Reflection.Metadata;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DocumentoRepository : GenericRepository<Documento>, IDocumentoRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private IConfiguration _appSettingsInstance;
        public DocumentoRepository(IApplicationDbContext applicationDbContext,
            IConfiguration appSettingsInstance) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _appSettingsInstance = appSettingsInstance;
        }

        public async Task<IEnumerable<DocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<DocumentoDto>? arrayDatos = new DocumentoDto[] { };
                string nombreFuncion = "sp_listado_documento";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DocumentoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DocumentoConEstadoDto>> BuscarListadoxArea(string? valor, string? parametro, int numeroPagina, int cantidadMostrar,string nsec_area)
        {
            try
            {
                IEnumerable<DocumentoConEstadoDto>? arrayDatos = new DocumentoConEstadoDto[] { };
                string nombreFuncion = "sp_listado_documentox_estado";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsec_area", nsec_area);
               

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DocumentoConEstadoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BusquedaDocumentoDto>> BuscarDocumento(BusquedaDocumento parametros)
        {
            try
            {
                IEnumerable<BusquedaDocumentoDto>? arrayDatos = new BusquedaDocumentoDto[] { };
                string nombreFuncion = "sp_traer_lista_documento";

                Hashtable htParametros = new Hashtable();
                htParametros.Add("_nsec_secretaria", parametros.nsec_secretaria);
                htParametros.Add("_nsec_gestion", parametros.nsec_gestion);
                htParametros.Add("_nsec_tipo_documento", parametros.nsec_tipo_documento);
                htParametros.Add("_codigo", parametros.codigo);
                htParametros.Add("_nsec_filtro_busqueda", parametros.nsec_filtro_busqueda);
                htParametros.Add("_valor_busqueda", parametros.valor_busqueda);
                htParametros.Add("numeropaginaactual", parametros.numeropaginaactual);
                htParametros.Add("cantidadmostrar", parametros.cantidadmostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<BusquedaDocumentoDto>(nombreFuncion, htParametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DetalleDocumentoSeccionDto>> BuscarDetalleSeccion(long nsec_documento)
        {
            try
            {
                IEnumerable<DetalleDocumentoSeccionDto>? arrayDatos = new DetalleDocumentoSeccionDto[] { };
                string nombreFuncion = "sp_traer_detalle_documento_seccion";

                Hashtable parametros = new Hashtable();
                
                parametros.Add("_nsec_documento", nsec_documento);
                

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DetalleDocumentoSeccionDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }



        //public async Task<IEnumerable<FiltroBusquedaDto>> TraerDetalleDocumento(int nsec_tipo_documento)
        //{
        //    try
        //    {
        //        IEnumerable<FiltroBusquedaDto>? arrayDatos = new FiltroBusquedaDto[] { };
        //        string nombreFuncion = "sp_listado_filtro_busqueda";

        //        Hashtable parametros = new Hashtable();
        //        parametros.Add("valor_bus", nsec_tipo_documento.ToString());
        //        parametros.Add("parametro_bus", "f.nsec_tipo_documento");

        //        arrayDatos = await _applicationDbContext.TraerArrayObjeto<FiltroBusquedaDto>(nombreFuncion, parametros);

        //        return arrayDatos;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}

