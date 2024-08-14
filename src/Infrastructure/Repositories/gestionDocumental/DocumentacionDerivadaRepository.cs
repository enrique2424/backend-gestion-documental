using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class DocumentacionDerivadaRepository : GenericRepository<DocumentacionDerivada>, IDocumentacionDerivadaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public DocumentacionDerivadaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<DocumentacionDerivadaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, long nsec_area_destino)
        {
            try
            {
                IEnumerable<DocumentacionDerivadaDto>? arrayDatos = new DocumentacionDerivadaDto[] { };
                string nombreFuncion = "sp_listado_recepcion_derivadas";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsec_area_destino",nsec_area_destino);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<DocumentacionDerivadaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

