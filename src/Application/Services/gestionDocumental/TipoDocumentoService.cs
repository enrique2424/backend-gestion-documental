using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class TipoDocumentoService : GenericService<TipoDocumento>,  ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;
        private readonly IFiltroBusquedaRepository _filtroBusquedaRepository;

        public TipoDocumentoService(ITipoDocumentoRepository tipoDocumentoRepository, IFiltroBusquedaRepository filtroBusquedaRepository) : base(tipoDocumentoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
            _filtroBusquedaRepository = filtroBusquedaRepository;
        }

        public async Task<RespuestaListado<TipoDocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<TipoDocumentoDto>(){
                response = await _tipoDocumentoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<TipoDocumentoMasterDto> TraerDocumentoMaster(int nsec_tipo_documento)
        {
            var tipoDocumento = await _tipoDocumentoRepository.BuscarPorNumSec(nsec_tipo_documento);
            var filtros = await _filtroBusquedaRepository.TraerFiltrosTipoDocumento(nsec_tipo_documento);
            var TipoDocumentoMasterDto = new TipoDocumentoMasterDto
            {
                tipoDocumento = tipoDocumento,
                filtroBusquedas = filtros.ToList()

            };
            return TipoDocumentoMasterDto;
        }
    }

}

