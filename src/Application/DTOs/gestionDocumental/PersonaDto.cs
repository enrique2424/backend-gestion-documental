using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class PersonaDto
    {
		public int nsec_tipo_persona { get; set; }
		public long num_sec { get; set; }
		public string? apellido_paterno { get; set; }
		public string? apellido_materno { get; set; }
		public string? nro_ci { get; set; }
		public string? complementario { get; set; }
		public string? celular { get; set; }
		public string? correo { get; set; }
		public string? direccion { get; set; }
		public string? dependencia { get; set; }
		public string? puesto { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }
		public string? nombre { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
