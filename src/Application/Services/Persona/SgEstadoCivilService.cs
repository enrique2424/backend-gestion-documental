
using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{
    public class SgEstadoCivilService :  ISgEstadoCivilService
    {
        private readonly ISgEstadoCivilRepository _sgEstadoCivilRepository;

        public SgEstadoCivilService(ISgEstadoCivilRepository sgEstadoCivilRepository) 
        {
            _sgEstadoCivilRepository = sgEstadoCivilRepository;
        }

        public async Task<RespuestaListado<SgEstadoCivilDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<SgEstadoCivilDto>(){
                response = await _sgEstadoCivilRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

       

        public async Task<SgEstadoCivil> BuscarPorNumSec(string numSec)
        {
            return await _sgEstadoCivilRepository.BuscarPorNumSec(long.Parse(numSec));
        }

        public Task<RespuestaDB> Eliminar( long numSec)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Guardar(SgEstadoCivil datos)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Modificar(SgEstadoCivil datos)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Modificar(long numsec, SgEstadoCivil datos)
        {
            throw new NotImplementedException();
        }
    }

}

