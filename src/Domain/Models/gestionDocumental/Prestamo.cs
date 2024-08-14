using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Prestamo
    {

        public long num_sec { get; set; }
        public string? cod_prestamo { get; set; }
		public long nsec_persona { get; set; }
		public string? nsec_area { get; set; }
		public long nsec_usuario { get; set; }
		public long nsec_estado_prestamo { get; set; }
		public DateTime? fecha_prestamo { get; set; }
		public DateTime? fecha_devolucion { get; set; }
		public string? nsec_secretaria { get; set; }
		public string telefono { get; set; }
		public string nro_interno { get; set; }
		public bool externo { get; set; }
		public string? obs_externo { get; set; }
		public string? cargo_persona { get; set; }
		public string? observacion { get; set; }
		
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }

	}
}
