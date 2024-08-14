using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class FiltroBusquedaService : GenericService<FiltroBusqueda>,  IFiltroBusquedaService
    {
        private readonly IFiltroBusquedaRepository _filtroBusquedaRepository;

        public FiltroBusquedaService(IFiltroBusquedaRepository filtroBusquedaRepository): base(filtroBusquedaRepository)
        {
            _filtroBusquedaRepository = filtroBusquedaRepository;
        }

        public async Task<RespuestaListado<FiltroBusquedaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<FiltroBusquedaDto>(){
                response = await _filtroBusquedaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaListado<FiltroBusquedaDto>> TraerListaFiltro(int nsec_tipo_documento)
        {
            var respuestaListado = new RespuestaListado<FiltroBusquedaDto>()
            {
                response = await _filtroBusquedaRepository.TraerListaFiltro(nsec_tipo_documento),
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

