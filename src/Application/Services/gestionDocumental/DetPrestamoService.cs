using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DetPrestamoService : GenericService<DetPrestamo>,  IDetPrestamoService
    {
        private readonly IDetPrestamoRepository _detPrestamoRepository;

        public DetPrestamoService(IDetPrestamoRepository detPrestamoRepository): base(detPrestamoRepository)
        {
            _detPrestamoRepository = detPrestamoRepository;
        }

        public async Task<RespuestaListado<DetPrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DetPrestamoDto>(){
                response = await _detPrestamoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

