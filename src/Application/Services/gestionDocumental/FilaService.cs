using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class FilaService : GenericService<Fila>,  IFilaService
    {
        private readonly IFilaRepository _filaRepository;

        public FilaService(IFilaRepository filaRepository): base(filaRepository)
        {
            _filaRepository = filaRepository;
        }

        public async Task<RespuestaListado<FilaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<FilaDto>(){
                response = await _filaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }
        public async Task<RespuestaListado<FilaDto>> TraerFilaXnec_estante(string? nsecEstante)
        {
            var respuestaListado = new RespuestaListado<FilaDto>()
            {
                response = await _filaRepository.TraerFilaXnec_estante(nsecEstante),
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

