using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DetSeccionFiltroBusquedaRepository : GenericRepository<DetSeccionFiltroBusqueda>, IDetSeccionFiltroBusquedaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public DetSeccionFiltroBusquedaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DetSeccionFiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<DetSeccionFiltroBusquedaDto>? arrayDatos = new DetSeccionFiltroBusquedaDto[] { };
                string nombreFuncion = "sp_listado_det_seccion_filtro_busqueda";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DetSeccionFiltroBusquedaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

