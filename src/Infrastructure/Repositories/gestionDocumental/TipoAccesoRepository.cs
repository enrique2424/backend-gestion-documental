using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class TipoAccesoRepository : GenericRepository<TipoAcceso>, ITipoAccesoRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public TipoAccesoRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<TipoAccesoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<TipoAccesoDto>? arrayDatos = new TipoAccesoDto[] { };
                string nombreFuncion = "sp_listado_tipo_acceso";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<TipoAccesoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

