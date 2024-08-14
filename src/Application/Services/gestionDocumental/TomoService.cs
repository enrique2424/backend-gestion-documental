using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class TomoService : GenericService<Tomo>,  ITomoService
    {
        private readonly ITomoRepository _tomoRepository;

        public TomoService(ITomoRepository tomoRepository): base(tomoRepository)
        {
            _tomoRepository = tomoRepository;
        }

        public async Task<RespuestaListado<TomoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<TomoDto>(){
                response = await _tomoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<TomoDto>> BuscarListadoNsecDocumento(string? nsecDocumento)
        {
            return (RespuestaListado<TomoDto>)await _tomoRepository.BuscarListadoNsecDocumento(nsecDocumento);
        }

    }

}

