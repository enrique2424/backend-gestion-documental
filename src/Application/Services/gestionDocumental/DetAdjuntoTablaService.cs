using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DetAdjuntoTablaService : GenericService<DetAdjuntoTabla>,  IDetAdjuntoTablaService
    {
        private readonly IDetAdjuntoTablaRepository _detAdjuntoTablaRepository;

        public DetAdjuntoTablaService(IDetAdjuntoTablaRepository detAdjuntoTablaRepository): base(detAdjuntoTablaRepository)
        {
            _detAdjuntoTablaRepository = detAdjuntoTablaRepository;
        }

        public async Task<RespuestaListado<DetAdjuntoTablaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DetAdjuntoTablaDto>(){
                response = await _detAdjuntoTablaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }
        public Task<IEnumerable<DetAdjuntoTablaDto>> BuscarArchivos(int nsec_seccion)
        {
            return _detAdjuntoTablaRepository.BuscarArchivos(nsec_seccion);
        }

    }

}

