using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Gestion
    {
        public long num_sec { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
