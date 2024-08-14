using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DetSeccionFiltroBusquedaService : GenericService<DetSeccionFiltroBusqueda>,  IDetSeccionFiltroBusquedaService
    {
        private readonly IDetSeccionFiltroBusquedaRepository _detSeccionFiltroBusquedaRepository;

        public DetSeccionFiltroBusquedaService(IDetSeccionFiltroBusquedaRepository detSeccionFiltroBusquedaRepository): base(detSeccionFiltroBusquedaRepository)
        {
            _detSeccionFiltroBusquedaRepository = detSeccionFiltroBusquedaRepository;
        }

        public async Task<RespuestaListado<DetSeccionFiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DetSeccionFiltroBusquedaDto>(){
                response = await _detSeccionFiltroBusquedaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

