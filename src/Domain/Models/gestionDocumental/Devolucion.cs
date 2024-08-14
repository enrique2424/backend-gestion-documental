using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Devolucion
    {
        public long num_sec { get; set; }
		public long nsec_prestamo { get; set; }
		public long nsec_persona { get; set; }
		public DateTime? fecha_devolucion { get; set; }
		public string? observacion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
