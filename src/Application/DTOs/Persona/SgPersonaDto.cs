using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class SgPersonaDto
    {
		public long num_sec { get; set; }
		public string? nombre { get; set; }
		public string? primer_apellido { get; set; }
		public string? segundo_apellido { get; set; }
		public string? tercer_apellido { get; set; }
		public string? nombre_completo { get; set; }

        public string? sexo { get; set; }
		public string? fecha_nacimiento { get; set; }
		public string? direccion { get; set; }		
		public string? correo { get; set; }
		public string? numero_documento_identificacion { get; set; }

        [JsonIgnore]
		public string? estado { get; set; }
		public string? codigo_funcionario { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
