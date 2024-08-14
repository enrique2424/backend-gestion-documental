using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class DetAdjuntoTabla
    {
        public long num_sec { get; set; }
		public long nsec_adjunto { get; set; }
		public long nsec_tabla { get; set; }
		public long nsec_seccion_documento { get; set; }		
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
