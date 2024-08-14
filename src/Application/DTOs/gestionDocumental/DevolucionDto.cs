using Domain.Models.GestionDocumental;
using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DevolucionDto
	{
		public long num_sec { get; set; }
		public long nsec_prestamo { get; set; }
		public long nsec_persona { get; set; }
		public string? fecha_devolucion { get; set; }
		public string? observacion { get; set; }
		[JsonIgnore]
		public string? estado { get; set; }

		[JsonIgnore]
		public int total { get; set; }
	}
	public class DevolucionMasterDto
	{
		public Prestamo? prestamo { get; set; }
		public List<traerDetallePorPrestamoDto>? detallePrestamo { get; set; }
		public Devolucion devolucion { get; set; }
	}
}
