using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DetDevolucionRepository : GenericRepository<DetDevolucion>, IDetDevolucionRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public DetDevolucionRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DetDevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<DetDevolucionDto>? arrayDatos = new DetDevolucionDto[] { };
                string nombreFuncion = "sp_listado_det_devolucion";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DetDevolucionDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DetDevolucion> TraerDetDevoXPrestamoDocumento(string? nsec_devolucion, string? nsec_documento)
        {
            try
            {
                DetDevolucion detDevolucion = new DetDevolucion { };
                string nombreFuncion = "sp_traer_det_devolucion_x_prestamo_documento";

                Hashtable parametros = new Hashtable();
                parametros.Add("_nsec_devolucion", long.Parse(nsec_devolucion));
                parametros.Add("_nsec_documento", long.Parse(nsec_documento));

                detDevolucion = await _applicationDbContext.TraerObjeto<DetDevolucion>(nombreFuncion, parametros);

                return detDevolucion;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

