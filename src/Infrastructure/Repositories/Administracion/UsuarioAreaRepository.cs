using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Domain.Models.Administracion;
using Infrastructure.Repositories.Common;
using System.Collections;
using Application.Interfaces.IData;
using Application.Utils;

namespace Infrastructure.Repositories.Administracion
{
    public class UsuarioAreaRepository : GenericRepositoryAdministracion<UsuarioArea>, IUsuarioAreaRepository
    {

        private readonly IApplicationDbContextAdministracion _applicationDbContextAdministracion;
       


        public UsuarioAreaRepository(IApplicationDbContextAdministracion applicationDbContextAdministracion) : base(applicationDbContextAdministracion)
        {
            _applicationDbContextAdministracion = applicationDbContextAdministracion;
         
        }

        public async Task<IEnumerable<UsuarioAreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<UsuarioAreaDto>? arrayDatos = new UsuarioAreaDto[] { };
                string nombreFuncion = "sp_listado_usuario_aplicacion_v2";

                int nsec_aplicacion = Convert.ToInt32(Aplicacion.NSEC_APLICACION);

                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsec_aplicacion", nsec_aplicacion);

                  arrayDatos = await _applicationDbContextAdministracion.TraerArrayObjeto<UsuarioAreaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UsuarioAreaDto>> BuscarListadoUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<UsuarioAreaDto>? arrayDatos = new UsuarioAreaDto[] { };
                string nombreFuncion = "sp_listado_usuario_v2";

                int nsec_aplicacion = Convert.ToInt32(Aplicacion.NSEC_APLICACION);
                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsec_aplicacion", nsec_aplicacion);

                arrayDatos = await _applicationDbContextAdministracion.TraerArrayObjeto<UsuarioAreaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UsuarioAreaDto>> BuscarListadoTodosUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<UsuarioAreaDto>? arrayDatos = new UsuarioAreaDto[] { };
                string nombreFuncion = "sp_listado_usuario_v3";

                int nsec_aplicacion = Convert.ToInt32(Aplicacion.NSEC_APLICACION);
                Hashtable parametros = new Hashtable();
                parametros.Add("valor_bus", valor == null ? "" : valor);
                parametros.Add("parametro_bus", parametro);
                parametros.Add("numeropaginaactual", numeroPagina);
                parametros.Add("cantidadmostrar", cantidadMostrar);
                parametros.Add("nsec_aplicacion", nsec_aplicacion);

                arrayDatos = await _applicationDbContextAdministracion.TraerArrayObjeto<UsuarioAreaDto>(nombreFuncion, parametros);

                return arrayDatos;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

