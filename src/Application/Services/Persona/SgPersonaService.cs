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

    public class SgPersonaService :  ISgPersonaService
    {
        private readonly ISgPersonaRepository _sgPersonaRepository;

        public SgPersonaService(ISgPersonaRepository sgPersonaRepository)
        {
            _sgPersonaRepository = sgPersonaRepository;
        }

        public async Task<RespuestaListado<SgPersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<SgPersonaDto>()
            {
                response = await _sgPersonaRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public Task<SgPersona> BuscarPorNumSec(long numSec)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Eliminar(long numsec)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDB> Guardar(SgPersona datos)
        {
            RespuestaDB respuestaBD = new RespuestaDB();

            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                respuestaBD = await _sgPersonaRepository.Guardar(datos);
                if (respuestaBD.status == "error")
                {
                    return respuestaBD;
                }
                transaction.Complete();

            }
            return respuestaBD;

        }


        public Task<RespuestaDB> Modificar(long numsec, SgPersona datos)
        {
            throw new NotImplementedException();
        }
    }

}
