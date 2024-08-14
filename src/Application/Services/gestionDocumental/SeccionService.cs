using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class SeccionService : GenericService<Seccion>,  ISeccionService
    {
        private readonly ISeccionRepository _seccionRepository;

        public SeccionService(ISeccionRepository seccionRepository): base(seccionRepository)
        {
            _seccionRepository = seccionRepository;
        }

        public async Task<RespuestaListado<SeccionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<SeccionDto>(){
                response = await _seccionRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<SeccionAdjuntoDto>> BuscarListadoNsecVolumen(string? nsec_documento)
        {
            return (RespuestaListado<SeccionAdjuntoDto>)await _seccionRepository.BuscarListadoNsecDocumento(nsec_documento);
        }

    }

}

