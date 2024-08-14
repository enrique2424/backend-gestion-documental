using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DetAdjuntoTablaDto
    {
		public long num_sec { get; set; }
		public long nsec_adjunto { get; set; }
		public long nsec_tabla { get; set; }
		public long nsec_seccion_documento { get; set; }
		public long nsec_usuario { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
