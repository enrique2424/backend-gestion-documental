using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class AreaDto
    {
		public string? num_sec { get; set; }
        public string? codigo { get; set; }
        public string? descripcion { get; set; }
        public string? dependencia { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
