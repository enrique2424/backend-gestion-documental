using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class AreaService : GenericService<Area>,  IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository): base(areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<RespuestaListado<AreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<AreaDto>(){
                response = await _areaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<AreaDto>> BuscarListadoxUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, int nsecUsuario)
        {
            var respuestaListado = new RespuestaListado<AreaDto>()
            {
                response = await _areaRepository.BuscarListadoxUsuario(valor, parametro, numeroPagina, cantidadMostrar, nsecUsuario),
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

