using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DetPrestamoDto
    {
		public long num_sec { get; set; }
		public long nsec_prestamo { get; set; }
		public long nsec_documento { get; set; }
		public bool estado_devolucion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
