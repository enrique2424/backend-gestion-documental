using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DetPrestamoRepository : GenericRepository<DetPrestamo>, IDetPrestamoRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public DetPrestamoRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DetPrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<DetPrestamoDto>? arrayDatos = new DetPrestamoDto[] { };
                string nombreFuncion = "sp_listado_det_prestamo";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DetPrestamoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<traerDetallePorPrestamoDto>> TraerDetallePorPrestamo(int nsec_prestamo)
        {
            try
            {
                IEnumerable<traerDetallePorPrestamoDto>? arrayDatos = new traerDetallePorPrestamoDto[] { };
                string nombreFuncion = "sp_traer_detalle_por_prestamo";

                Hashtable parametros = new Hashtable();
                parametros.Add("_nsec_prestamo", nsec_prestamo);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<traerDetallePorPrestamoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
        

    }
}

