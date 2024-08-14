using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;
using System.Transactions;

namespace Application.Services.GestionDocumental
{

    public class SgDocumentoIdentificacionService : ISgDocumentoIdentificacionService
    {
        private readonly ISgDocumentoIdentificacionRepository _sgDocumentoIdentificacionRepository;

        public SgDocumentoIdentificacionService(ISgDocumentoIdentificacionRepository sgDocumentoIdentificacionRepository)
        {
            _sgDocumentoIdentificacionRepository = sgDocumentoIdentificacionRepository;
        }

        public async Task<RespuestaListado<SgDocumentoIdentificacionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<SgDocumentoIdentificacionDto>()
            {
                response = await _sgDocumentoIdentificacionRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        

        

        public async Task<SgDocumentoIdentificacion> BuscarPorNumSec(string numSec)
        {
            return await _sgDocumentoIdentificacionRepository.BuscarPorNumSec(long.Parse(numSec));
        }

        public Task<RespuestaDB> delete(long numSec)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Guardar(SgDocumentoIdentificacion datos)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Modificar(long numsec, SgDocumentoIdentificacion datos)
        {
            throw new NotImplementedException();
        }
    }

}

