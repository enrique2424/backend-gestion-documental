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
    public class LoginRepository : ILoginRepository
    {
        private readonly AdministracionContext _administracionContext;
        public LoginRepository(AdministracionContext administracionContext)
        {
            _administracionContext = administracionContext;
        }

        public async Task<Usuario> BuscarUsuario(UsuarioDto usuarioDto, long nsec_aplicacion)
        {

            string nombreFuncion = "sp_autentificar";
            Usuario usuario;

            using (var cnx = this._administracionContext.CreateConnection())
            {
                usuario = await cnx.QuerySingleOrDefaultAsync<Usuario>(
                    sql: nombreFuncion,
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        _cuenta = usuarioDto.cuenta,
                        _nsec_aplicacion = nsec_aplicacion
                    }
                );
            }

            return usuario;
        }
    }
}
