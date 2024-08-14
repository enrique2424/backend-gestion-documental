using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Domain.Models.Administracion;
using Infrastructure.Persistence;
using Dapper;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Administracion
{
    public class MenuRepository : IMenuRepository
    {
        private readonly AdministracionContext _administracionContext;
        public MenuRepository(AdministracionContext administracionContext)
        {
            _administracionContext = administracionContext;
        }
        public async Task<IEnumerable<MenuDto>> TraerMenuPorUsuario(long num_sec, string cuenta, string routerlink, long nsec_aplicacion)
        {
            try
            {
                IEnumerable<MenuDto> arrayDatos = new MenuDto[] { };
                string nombreFuncion = "sp_traer_menu_por_usuario";

                using (var cnx = this._administracionContext.CreateConnection())
                {
                    arrayDatos = await cnx.QueryAsync<MenuDto>(
                        sql: nombreFuncion,
                        commandType: CommandType.StoredProcedure,
                        param: new
                        {
                            _num_sec = num_sec,
                            _cuenta = cuenta,
                            _routerlink = routerlink,
                            _nsec_aplicacion = nsec_aplicacion
                        }
                    );
                }

                return arrayDatos;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
