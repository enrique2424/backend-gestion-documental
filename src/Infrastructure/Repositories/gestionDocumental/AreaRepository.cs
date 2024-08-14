using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using Application.Utils;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AreaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<AreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<AreaDto>? arrayDatos = new AreaDto[] { };
                string nombreFuncion = "sp_listado_area_v2";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<AreaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AreaDto>> BuscarListadoxUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, int nsecUsuario)
        {
            try
            {
                IEnumerable<AreaDto>? arrayDatos = new AreaDto[] { };
                string nombreFuncion = "sp_listado_area_usuario_v2";
                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsecusuario", nsecUsuario);
        

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<AreaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

