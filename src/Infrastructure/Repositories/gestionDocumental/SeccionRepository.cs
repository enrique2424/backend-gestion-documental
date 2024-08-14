using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class SeccionRepository : GenericRepository<Seccion>, ISeccionRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public SeccionRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<SeccionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<SeccionDto>? arrayDatos = new SeccionDto[] { };
                string nombreFuncion = "sp_listado_seccion";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<SeccionDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SeccionAdjuntoDto>> BuscarListadoNsecDocumento(string? nsec_documento)
        {
            try
            {
                string nombreFuncion = "sp_listado_detalle_seccion";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", nsec_documento == null ? "" : nsec_documento);
                parametros.Add("parametro_bus", "t.nsec_documento");                

                var datos = await this._applicationDbContext.TraerArrayObjeto<SeccionAdjuntoDto>(nombreFuncion, parametros);
                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<SeccionAdjuntoDto>> BuscarListadoNsecDocumentoObjeto(string? nsec_documento)
        {
            try
            {
                string nombreFuncion = "sp_listado_detalle_seccion_objeto";

                Hashtable parametros = new Hashtable();
                parametros.Add("_nsec_documento", nsec_documento);

                var datos = await this._applicationDbContext.TraerArrayObjeto<SeccionAdjuntoDto>(nombreFuncion, parametros);
                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}

