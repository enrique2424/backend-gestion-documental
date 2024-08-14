using Domain.Models.GestionDocumental;
using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{	
	//PrestamoDto es el formato que llega para el buscar prestamos
	public class PrestamoDto
    {
		public long num_sec { get; set; }
		public string? cod_prestamo { get; set; }
		public string? nombre {get; set; }
		public string? apellido_paterno {get; set; }
		public string? apellido_materno {get; set; }
        public string? nombre_completo { get; set; }
        public string? cargo { get; set; }
		public string? fecha_prestamo { get; set; }
		public string? fecha_devolucion { get; set; }
		public string? observaciones { get; set; }
		public string? secretaria { get; set; }
		public string? area { get; set; }
		public string? estado { get; set; }

		public string? telefono { get; set; }
		public string? nro_interno { get; set; }
		public Boolean externo { get; set; }
		public string? obs_externo { get; set; }



		[JsonIgnore]
        public int total { get; set; }
    }

	public class PrestamoMasterDto
    {
		public Prestamo? prestamo { get; set; }
		public List<traerDetallePorPrestamoDto>? detallePrestamo { get; set; }
    }
}
