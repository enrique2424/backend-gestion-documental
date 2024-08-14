using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.IServices.Administracion;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.Administracion;
using Application.DTOs.Administracion;

namespace Application.Services.Administracion
{

    public class UsuarioAreaService : GenericServiceAdmnistracion<UsuarioArea>, IUsuarioAreaService
    {
        private readonly IUsuarioAreaRepository _usuarioareaRepository;

        public UsuarioAreaService(IUsuarioAreaRepository usuarioAreaRepository) : base(usuarioAreaRepository)
        {
            _usuarioareaRepository = usuarioAreaRepository;
        }

        public async Task<RespuestaListado<UsuarioAreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<UsuarioAreaDto>(){
                response = await _usuarioareaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<UsuarioAreaDto>> BuscarListadoUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<UsuarioAreaDto>()
            {
                response = await _usuarioareaRepository.BuscarListadoUsuario(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<UsuarioAreaDto>> BuscarListadoTodosUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<UsuarioAreaDto>()
            {
                response = await _usuarioareaRepository.BuscarListadoTodosUsuario(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

    }

}

