using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class TomoDto
    {
		public long num_sec { get; set; }
		public long nsec_documento { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
