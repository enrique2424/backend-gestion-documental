using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DetDevolucionDto
    {
		public long num_sec { get; set; }
		public long nsec_devolucion { get; set; }
		public long nsec_documento { get; set; }
		public string? observacion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
