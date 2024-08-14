using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class DetDevolucion
    {
        public long num_sec { get; set; }
		public long nsec_devolucion { get; set; }
		public long nsec_documento { get; set; }
		public string? observacion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
