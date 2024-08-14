using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class PrestamoRepository : GenericRepository<Prestamo>, IPrestamoRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public PrestamoRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<PrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<PrestamoDto>? arrayDatos = new PrestamoDto[] { };
                string nombreFuncion = "sp_listado_prestamo";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<PrestamoDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Prestamo> BuscarPorNumSecXcodPrestamo(string? cod_prestamo)
        {
            try
            {
                Prestamo Datos = new Prestamo { };
                string nombreFuncion = "sp_traer_prestamo_x_cod_prestamo";

                Hashtable parametros = new Hashtable();
                parametros.Add("_cod_prestamo", cod_prestamo);

                Datos = await _applicationDbContext.TraerObjeto<Prestamo>(nombreFuncion, parametros);

                return Datos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

