using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class TipoPersonaService : GenericService<TipoPersona>,  ITipoPersonaService
    {
        private readonly ITipoPersonaRepository _tipoPersonaRepository;

        public TipoPersonaService(ITipoPersonaRepository tipoPersonaRepository): base(tipoPersonaRepository)
        {
            _tipoPersonaRepository = tipoPersonaRepository;
        }

        public async Task<RespuestaListado<TipoPersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<TipoPersonaDto>(){
                response = await _tipoPersonaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

