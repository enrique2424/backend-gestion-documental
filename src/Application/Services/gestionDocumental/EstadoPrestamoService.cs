using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class EstadoPrestamoService : GenericService<EstadoPrestamo>,  IEstadoPrestamoService
    {
        private readonly IEstadoPrestamoRepository _estadoPrestamoRepository;

        public EstadoPrestamoService(IEstadoPrestamoRepository estadoPrestamoRepository): base(estadoPrestamoRepository)
        {
            _estadoPrestamoRepository = estadoPrestamoRepository;
        }

        public async Task<RespuestaListado<EstadoPrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<EstadoPrestamoDto>(){
                response = await _estadoPrestamoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

