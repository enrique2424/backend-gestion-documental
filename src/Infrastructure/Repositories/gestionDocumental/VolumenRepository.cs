using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class VolumenRepository : GenericRepository<Volumen>, IVolumenRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public VolumenRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<VolumenDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<VolumenDto>? arrayDatos = new VolumenDto[] { };
                string nombreFuncion = "sp_listado_volumen";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<VolumenDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VolumenDto>> BuscarListadoNsecDocumento(string? valor)
        {
            try
            {
                string nombreFuncion = "sp_listado_volumen";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", "t.nsec_documento");
                parametros.Add("numeropaginaactual", 0);
                parametros.Add("cantidadmostrar", 10);

                var datos = await this._applicationDbContext.TraerArrayObjeto<VolumenDto>(nombreFuncion, parametros);
                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

