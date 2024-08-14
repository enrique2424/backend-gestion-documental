using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DetDevolucionService : GenericService<DetDevolucion>, IDetDevolucionService
    {
        private readonly IDetDevolucionRepository _detDevolucionRepository;

        public DetDevolucionService(IDetDevolucionRepository detDevolucionRepository): base(detDevolucionRepository)
        {
            _detDevolucionRepository = detDevolucionRepository;
        }

        public async Task<RespuestaListado<DetDevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DetDevolucionDto>(){
                response = await _detDevolucionRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }
        public async Task<DetDevolucion> TraerDetDevoXPrestamoDocumento(string? nsec_devolucion, string? nsec_documento)
        {
            DetDevolucion respuestaDB = await _detDevolucionRepository.TraerDetDevoXPrestamoDocumento(nsec_devolucion, nsec_documento);
            

            //if (respuestaListado.response.Count() > 0)
            //{
            //    int elementosTotales = respuestaListado.response.ElementAt(0).total;
            //    respuestaListado.total = elementosTotales;
            //}

            return respuestaDB;
        }

    }

}

