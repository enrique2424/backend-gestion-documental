using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class SecretariaService : GenericService<Secretaria>,  ISecretariaService
    {
        private readonly ISecretariaRepository _secretariaRepository;

        public SecretariaService(ISecretariaRepository secretariaRepository): base(secretariaRepository)
        {
            _secretariaRepository = secretariaRepository;
        }

        public async Task<RespuestaListado<SecretariaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<SecretariaDto>(){
                response = await _secretariaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
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

