using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class SgDocumentoIdentificacion
    {
        public long num_sec { get; set; }
		public string? nombre { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
