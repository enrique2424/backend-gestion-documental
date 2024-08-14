using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class SecretariaRepository : GenericRepository<Secretaria>, ISecretariaRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public SecretariaRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<SecretariaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<SecretariaDto>? arrayDatos = new SecretariaDto[] { };
                string nombreFuncion = "sp_listado_secretaria";

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);

                arrayDatos = await _applicationDbContext.TraerArrayObjeto<SecretariaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

