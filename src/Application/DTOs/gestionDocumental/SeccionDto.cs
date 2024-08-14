using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class SeccionDto
    {
		public long num_sec { get; set; }
		public int nro_orden_seccion { get; set; }
		public long nsec_volumen { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
