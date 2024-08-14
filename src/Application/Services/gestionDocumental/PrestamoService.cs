using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class PrestamoService : GenericService<Prestamo>,  IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IDetPrestamoRepository _detPrestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository,IDetPrestamoRepository detPrestamoRepository): base(prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
            _detPrestamoRepository = detPrestamoRepository;
        }

        public async Task<RespuestaListado<PrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<PrestamoDto>(){
                response = await _prestamoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<PrestamoMasterDto> TraerPrestamoMaster(int nsec_prestamo)
        {
            var prestamo = await _prestamoRepository.BuscarPorNumSec(nsec_prestamo);
            var detalle = await _detPrestamoRepository.TraerDetallePorPrestamo(nsec_prestamo);
            var PrestamoMasterDto = new PrestamoMasterDto
            {
                prestamo = prestamo,
                detallePrestamo = detalle.ToList()

            };
            return PrestamoMasterDto;
        }
        public async Task<IEnumerable<traerDetallePorPrestamoDto>> TraerDetallePrestamoPorPrestamo(int nsec_prestamo)
        {
            
            var res = await _detPrestamoRepository.TraerDetallePorPrestamo(nsec_prestamo);

            List<traerDetallePorPrestamoDto> lista = res.ToList();
      

            return lista;
        }

        public async Task<PrestamoMasterDto> TraerPrestamoMasterXcodPrestamo(string cod_prestamo)
        {

            Prestamo prestamo = await _prestamoRepository.BuscarPorNumSecXcodPrestamo(cod_prestamo);
            if (prestamo==null)
            {
                var master = new PrestamoMasterDto
                {
                    prestamo = null,
                    detallePrestamo = null
                };
                return master;
            }
            else { 
                var detalle = await _detPrestamoRepository.TraerDetallePorPrestamo(Convert.ToInt32(prestamo.num_sec));
                var PrestamoMasterDto = new PrestamoMasterDto
                {
                    prestamo = prestamo,
                    detallePrestamo = detalle.ToList()

                };
                return PrestamoMasterDto;
            }
        }
    }

}

