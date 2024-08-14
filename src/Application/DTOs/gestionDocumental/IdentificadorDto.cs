using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class IdentificadorDto
    {
		public long num_sec { get; set; }
		public string? descripcion { get; set; }
		public string? tipo_dato { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
