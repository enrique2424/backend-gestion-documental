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

    public class RolService : GenericServiceAdmnistracion<Rol>, IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository) : base(rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<RespuestaListado<RolDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<RolDto>(){
                response = await _rolRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

