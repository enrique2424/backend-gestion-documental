using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DetAdjuntoTablaRepository : GenericRepository<DetAdjuntoTabla>, IDetAdjuntoTablaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public DetAdjuntoTablaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DetAdjuntoTablaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<DetAdjuntoTablaDto>? arrayDatos = new DetAdjuntoTablaDto[] { };
                string nombreFuncion = "sp_listado_det_adjunto_tabla";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DetAdjuntoTablaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DetAdjuntoTablaDto>> BuscarArchivos(int nsec_seccion)
        {
            try
            {
                IEnumerable<DetAdjuntoTablaDto>? arrayDatos = new DetAdjuntoTablaDto[] { };
                string nombreFuncion = "traerdetalle DetAdjuntoTabla";

                Hashtable parametros = new Hashtable();
                parametros.Add("_nsec_seccion", nsec_seccion);               

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DetAdjuntoTablaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}

