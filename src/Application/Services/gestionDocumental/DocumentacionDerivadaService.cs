using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DocumentacionDerivadaService : GenericService<DocumentacionDerivada>,  IDocumentacionDerivadaService
    {
        private readonly IDocumentacionDerivadaRepository _sgaDocumentacionDerivadaRepository;

        public DocumentacionDerivadaService(IDocumentacionDerivadaRepository sgaDocumentacionDerivadaRepository): base(sgaDocumentacionDerivadaRepository)
        {
            _sgaDocumentacionDerivadaRepository = sgaDocumentacionDerivadaRepository;
        }

        public async Task<RespuestaListado<DocumentacionDerivadaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, long nsec_area_destino)
        {
            var respuestaListado = new RespuestaListado<DocumentacionDerivadaDto>(){
                response = await _sgaDocumentacionDerivadaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar, nsec_area_destino),
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

