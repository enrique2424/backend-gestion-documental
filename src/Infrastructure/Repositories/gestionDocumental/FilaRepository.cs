using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class FilaRepository : GenericRepository<Fila>, IFilaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public FilaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<FilaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<FilaDto>? arrayDatos = new FilaDto[] { };
                string nombreFuncion = "sp_listado_fila";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<FilaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<FilaDto>> TraerFilaXnec_estante(string? nsecEstante)
        {
            try
            {
                string nombreFuncion = "sp_traer_fila_por_nsecEstante";

                Hashtable parametros = new Hashtable();
                parametros.Add("_num_sec", nsecEstante == null ? 0 : long.Parse(nsecEstante));              

                var datos = await this._applicationDbContext.TraerArrayObjeto<FilaDto>(nombreFuncion, parametros);
                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

