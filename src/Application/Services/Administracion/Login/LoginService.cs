using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.ISecurity;
using Application.Interfaces.IServices.Administracion;
using Domain.Models.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Administracion.Login
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private IPasswordHasher _passwordHasher;
        private readonly long NSEC_APLICACION;
        public LoginService(ILoginRepository loginRepository, IPasswordHasher passwordHasher)
        {
            _loginRepository = loginRepository;
            
            _passwordHasher = passwordHasher;
            NSEC_APLICACION = Utils.Aplicacion.NSEC_APLICACION;
        }
        public async Task<Usuario?> ValidateUser(UsuarioDto usuarioDto)
        {
            var usuario = await _loginRepository.BuscarUsuario(usuarioDto, NSEC_APLICACION);
            if (usuario != null)
            {
                if (PasswordValido(usuario, usuarioDto.contrasena))
                {
                    return usuario;
                }
            }

            return null;
        }
        public async Task<Usuario> Populate(string cuenta)
        {
            var usuarioDto = new UsuarioDto { cuenta = cuenta, contrasena = "" };
            return await _loginRepository.BuscarUsuario(usuarioDto, NSEC_APLICACION);
        }

        private bool PasswordValido(Usuario usuario, string contrasena)
        {
            if (usuario != null)
            {
                return (usuario.hash!.SequenceEqual(_passwordHasher.Hash(contrasena!, usuario.salt!)));
            }
            return false;

        }


    }
}
