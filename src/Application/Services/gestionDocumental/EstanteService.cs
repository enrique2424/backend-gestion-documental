using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class EstanteService : GenericService<Estante>,  IEstanteService
    {
        private readonly IEstanteRepository _estanteRepository;

        public EstanteService(IEstanteRepository estanteRepository): base(estanteRepository)
        {
            _estanteRepository = estanteRepository;
        }

        public async Task<RespuestaListado<EstanteDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<EstanteDto>(){
                response = await _estanteRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        //Task<RespuestaListado<EstanteDto>> IEstanteService.BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        //{
        //    throw new NotImplementedException();
        //}
    }

}

