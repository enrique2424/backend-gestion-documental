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

    public class UserRolService : GenericServiceAdmnistracion<UserRol>, IUserRolService
    {
        private readonly IUserRolRepository _userrolRepository;

        public UserRolService(IUserRolRepository userrolRepository) : base(userrolRepository)
        {
            _userrolRepository = userrolRepository;
        }

        public async Task<RespuestaListado<UserRolDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<UserRolDto>(){
                response = await _userrolRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

