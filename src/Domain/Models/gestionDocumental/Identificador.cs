using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Identificador
    {
        public long num_sec { get; set; }
		public string? descripcion { get; set; }
		public string? tipo_dato { get; set; }
       [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
