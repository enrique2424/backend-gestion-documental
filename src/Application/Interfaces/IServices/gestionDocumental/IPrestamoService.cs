using Application.DTOs.GestionDocumental;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.GestionDocumental
{
    public interface IPrestamoService : IGenericService<Prestamo>
    {
        public Task<RespuestaListado<PrestamoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<PrestamoMasterDto> TraerPrestamoMaster(int nsec_prestamo);
        public Task<IEnumerable<traerDetallePorPrestamoDto>> TraerDetallePrestamoPorPrestamo(int nsec_prestamo);
        public Task<PrestamoMasterDto> TraerPrestamoMasterXcodPrestamo(string cod_prestamo);
        
    }
}
