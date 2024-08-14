using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class FiltroBusquedaRepository : GenericRepository<FiltroBusqueda>, IFiltroBusquedaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public FiltroBusquedaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<FiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<FiltroBusquedaDto>? arrayDatos = new FiltroBusquedaDto[] { };
                string nombreFuncion = "sp_listado_filtro_busqueda";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<FiltroBusquedaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<FiltroBusqueda>> TraerFiltrosTipoDocumento(int nsec_tipo_documento)
        {
            try
            {
                IEnumerable<FiltroBusqueda>? arrayDatos = new FiltroBusqueda[] { };
                string nombreFuncion = "sp_traer_filtros_tipo_documento";

                Hashtable parametros = new Hashtable();
                parametros.Add("_nsec_tipo_documento", nsec_tipo_documento);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<FiltroBusqueda>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
            public async Task<IEnumerable<FiltroBusquedaDto>> TraerListaFiltro(int nsec_tipo_documento)
        {
            try
            {
                IEnumerable<FiltroBusquedaDto>? arrayDatos = new FiltroBusquedaDto[] { };
                string nombreFuncion = "sp_listado_filtro_busqueda";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", nsec_tipo_documento.ToString());
                parametros.Add("parametro_bus", "f.nsec_tipo_documento");
                
                

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<FiltroBusquedaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }       

    }
}

