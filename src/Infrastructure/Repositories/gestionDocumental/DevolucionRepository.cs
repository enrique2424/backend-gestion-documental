using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DevolucionRepository : GenericRepository<Devolucion>, IDevolucionRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public DevolucionRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<DevolucionDto>? arrayDatos = new DevolucionDto[] { };
                string nombreFuncion = "sp_listado_devolucion";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DevolucionDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Devolucion> TraerDevXNsecPrestamo(string? nsec_prestamo)
        {
            try
            {
                Devolucion Datos = new Devolucion { };
                string nombreFuncion = "sp_traer_devolucion_x_nsec_prestamo";

                Hashtable parametros = new Hashtable();
                parametros.Add("_nsec_prestamo", long.Parse(nsec_prestamo));

                Datos = await _applicationDbContext.TraerObjeto<Devolucion>(nombreFuncion, parametros);

                return Datos;

            }
            catch (Exception)
            {
                throw; 
            }
        }

    }
}

