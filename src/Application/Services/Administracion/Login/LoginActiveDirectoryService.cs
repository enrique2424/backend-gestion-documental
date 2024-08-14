using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.IServices.Administracion;
using Domain.Models.Administracion;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Administracion.Login
{
    public class LoginActiveDirectoryService : ILoginService
    {
        private readonly LoginOption _loginOption;
        private readonly ILoginRepository _loginRepository;
        private readonly long NSEC_APLICACION;
        public LoginActiveDirectoryService(ILoginRepository loginRepository, IOptions<LoginOption> loginOption)
        {
            _loginRepository = loginRepository;
            _loginOption = loginOption.Value;
            NSEC_APLICACION = Utils.Aplicacion.NSEC_APLICACION;

        }

        public async Task<Usuario> Populate(string cuenta)
        {
            var usuarioDto = new UsuarioDto { cuenta = cuenta, contrasena = "" };
            return await _loginRepository.BuscarUsuario(usuarioDto, NSEC_APLICACION);
        }

        public async Task<Usuario?> ValidateUser(UsuarioDto usuarioDto)
        {
            if (IsAuthenticatedActiveDirectory(usuarioDto.cuenta, usuarioDto.contrasena))
            {
                var usuario = await _loginRepository.BuscarUsuario(usuarioDto, NSEC_APLICACION);
                if (usuario != null)
                {
                    return usuario;

                }
            }
            return null;
        }

        private bool IsAuthenticatedActiveDirectory(string cuenta, string contrasena)
        {

            try
            {
                var domain = _loginOption.Domain;
                string domainAndUsername = domain + @"\" + cuenta;
                var ldap = _loginOption.IpServer;
                using (var cn = new LdapConnection())
                {
                    cn.Connect(ldap, 389);
                    cn.Bind(domainAndUsername, contrasena);
                    return true;
                }

            }
            catch
            {
                return false;
            }

        }
    }
}
