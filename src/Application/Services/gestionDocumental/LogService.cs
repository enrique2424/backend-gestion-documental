using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class LogService : GenericService<Log>,  ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository): base(logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<RespuestaListado<LogDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<LogDto>(){
                response = await _logRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

