using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Domain.Models.Administracion;
using Infrastructure.Repositories.Common;
using System.Collections;
using Application.Interfaces.IData;
using Application.Utils;

namespace Infrastructure.Repositories.Administracion
{
    public class UserRolRepository : GenericRepositoryAdministracion<UserRol>, IUserRolRepository
    {

        private readonly IApplicationDbContextAdministracion _applicationDbContextAdministracion;
       


        public UserRolRepository(IApplicationDbContextAdministracion applicationDbContextAdministracion) : base(applicationDbContextAdministracion)
        {
            _applicationDbContextAdministracion = applicationDbContextAdministracion;
         
        }

        public async Task<IEnumerable<UserRolDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<UserRolDto>? arrayDatos = new UserRolDto[] { };
                string nombreFuncion = "sp_listado_user_rol";

                int nsec_aplicacion = Convert.ToInt32(Aplicacion.NSEC_APLICACION);

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsec_aplicacion", nsec_aplicacion);

                arrayDatos = await _applicationDbContextAdministracion.TraerArrayObjeto<UserRolDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

