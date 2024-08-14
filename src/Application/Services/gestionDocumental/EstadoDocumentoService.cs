using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class EstadoDocumentoService : GenericService<EstadoDocumento>,  IEstadoDocumentoService
    {
        private readonly IEstadoDocumentoRepository _estadoDocumentoRepository;

        public EstadoDocumentoService(IEstadoDocumentoRepository estadoDocumentoRepository): base(estadoDocumentoRepository)
        {
            _estadoDocumentoRepository = estadoDocumentoRepository;
        }

        public async Task<RespuestaListado<EstadoDocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<EstadoDocumentoDto>(){
                response = await _estadoDocumentoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

