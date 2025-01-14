using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class SgDocumentoIdentificacionDto
    {
		public long num_sec { get; set; }
		public string? nombre { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
