using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class AdjuntoDto
    {
		public long num_sec { get; set; }
		public int tamano { get; set; }
		public long nsec_usuario { get; set; }
		public string? nombre { get; set; }
		public string? nombre_fisico { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }
		public string? content_type { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
