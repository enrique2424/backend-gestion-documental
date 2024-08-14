using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class PersonaService : GenericService<Persona>,  IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository): base(personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<RespuestaListado<PersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<PersonaDto>(){
                response = await _personaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public Task Delete(RespuestaDB persona)
        {
            throw new NotImplementedException();
        }
    }

}

