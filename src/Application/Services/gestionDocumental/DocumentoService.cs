using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DocumentoService : GenericService<Documento>,  IDocumentoService
    {
        private readonly IDocumentoRepository _documentoRepository;
        private readonly ITomoRepository _tomoRepository;
        private readonly IVolumenRepository _volumenRepository;
        private readonly ISeccionRepository _seccionRepository;

        public DocumentoService(IDocumentoRepository documentoRepository
            , ITomoRepository tomoRepository
            , IVolumenRepository volumenRepository
            , ISeccionRepository seccionRepository
            ): base(documentoRepository)
        {
            _documentoRepository = documentoRepository;
            _tomoRepository = tomoRepository;
            _volumenRepository = volumenRepository;
            _seccionRepository = seccionRepository;
        }

        public async Task<RespuestaListado<DocumentoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DocumentoDto>(){
                response = await _documentoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<DocumentoConEstadoDto>> BuscarListadoxArea(string? valor, string? parametro, int numeroPagina, int cantidadMostrar,string nsec_area)
        {
            var respuestaListado = new RespuestaListado<DocumentoConEstadoDto>()
            {
                response = await _documentoRepository.BuscarListadoxArea(valor, parametro, numeroPagina, cantidadMostrar,nsec_area),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }


        public async Task<DetalleDocumentoDto> TraerdetalleDocumento(string nsec_documento)
        {
            List<TomoDto> arrayTomo = (await _tomoRepository.BuscarListadoNsecDocumento(nsec_documento)).ToList();
            List<VolumenDto> arrayVolumen = (await _volumenRepository.BuscarListadoNsecDocumento(nsec_documento)).ToList();
            List<SeccionAdjuntoDto> arraySeccion = (await _seccionRepository.BuscarListadoNsecDocumentoObjeto(nsec_documento)).ToList();

            DetalleDocumentoDto detalleDto = new DetalleDocumentoDto()
            {
                arrayTomo = arrayTomo,
                arrayVolumen = arrayVolumen,
                arrarySeccion = arraySeccion,
            };

            //List<SeccionDto> arraySeccion = (await _seccionRepository.BuscarListado("1", "s.nsec_volumen", 0, 10)).ToList();// filtrar por nsec_documento

            //DetalleVolumenDto detalleVolumen = new DetalleVolumenDto()
            //{
            //    arraryDetSeccion = arraySeccion,
            //};

            //DetalleTomoDto detalleTomo = new DetalleTomoDto()
            //{
            //    arraryDetVolumen = detalleVolumen,



            //};

            return detalleDto;
        }

        public async Task<RespuestaListado<BusquedaDocumentoDto>> BuscarDocumento(BusquedaDocumento parametros)
        {
            var respuestaListado = new RespuestaListado<BusquedaDocumentoDto>()
            {
                response = await _documentoRepository.BuscarDocumento(parametros),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<IEnumerable<DetalleDocumentoSeccionDto>> BuscarDetalleSeccion(long nsec_documento)
        {
            var detalleDocumentoSeccion = await _documentoRepository.BuscarDetalleSeccion(nsec_documento);
            return detalleDocumentoSeccion;
        }
    }

}

