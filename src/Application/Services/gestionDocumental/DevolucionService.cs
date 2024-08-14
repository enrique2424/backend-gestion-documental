using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Services.GestionDocumental
{

    public class DevolucionService : GenericService<Devolucion>, IDevolucionService
    {
        private readonly IDevolucionRepository _devolucionRepository;
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IDetPrestamoRepository _detPrestamoRepository;


        public DevolucionService(IDevolucionRepository devolucionRepository,
            IPrestamoRepository prestamoRepository,
            IDetPrestamoRepository detPrestamoRepository) : base(devolucionRepository)
        {
            _prestamoRepository = prestamoRepository;
            _devolucionRepository = devolucionRepository;
            _detPrestamoRepository = detPrestamoRepository;
        }

        public async Task<RespuestaListado<DevolucionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<DevolucionDto>()
            {
                response = await _devolucionRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }
        public async Task<DevolucionMasterDto> TraerDevXNsecPrestamo(string cod_prestamo)
        {
            Prestamo prestamo = await _prestamoRepository.BuscarPorNumSecXcodPrestamo(cod_prestamo);
            if (prestamo == null)
            {
                var master = new DevolucionMasterDto
                {
                    prestamo = null,
                    detallePrestamo = null
                };
                return master;
            }
            else
            {
                var detalle = await _detPrestamoRepository.TraerDetallePorPrestamo(Convert.ToInt32(prestamo.num_sec));
                var devolucion = await _devolucionRepository.TraerDevXNsecPrestamo(prestamo.num_sec.ToString());
                var DevolucionMasterDto = new DevolucionMasterDto
                {
                    prestamo = prestamo,
                    detallePrestamo = detalle.ToList(),
                    devolucion=devolucion

                };
                    return DevolucionMasterDto;
            }
        }


    }
}

