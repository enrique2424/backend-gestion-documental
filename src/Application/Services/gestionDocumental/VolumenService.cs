using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class VolumenService : GenericService<Volumen>,  IVolumenService
    {
        private readonly IVolumenRepository _volumenRepository;

        public VolumenService(IVolumenRepository volumenRepository): base(volumenRepository)
        {
            _volumenRepository = volumenRepository;
        }

        public async Task<RespuestaListado<VolumenDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<VolumenDto>(){
                response = await _volumenRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }
        public async Task<VolumenDto> BuscarListadoNsecTomo(string? nsecTomo)
        {
            return (VolumenDto)await _volumenRepository.BuscarListadoNsecDocumento(nsecTomo);
        }

    }

}

