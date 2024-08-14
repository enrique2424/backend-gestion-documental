using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class TomoRepository : GenericRepository<Tomo>, ITomoRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public TomoRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<TomoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<TomoDto>? arrayDatos = new TomoDto[] { };
                string nombreFuncion = "sp_listado_tomo";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<TomoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TomoDto>> BuscarListadoNsecDocumento(string? nsecDocumento)
        {
            try
            {
                string nombreFuncion = "sp_listado_tomo";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", nsecDocumento == null ? "" : nsecDocumento);
                parametros.Add("parametro_bus", "t.nsec_documento");
                parametros.Add("numeropaginaactual", 0);
                parametros.Add("cantidadmostrar", 10);

                var datos = await this._applicationDbContext.TraerArrayObjeto<TomoDto>(nombreFuncion, parametros);
                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }

      
    }
}

