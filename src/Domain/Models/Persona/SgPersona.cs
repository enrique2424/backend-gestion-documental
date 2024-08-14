using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class SgPersona
    {
        public long num_sec { get; set; }
		public string? nombre { get; set; }
		public string? primer_apellido { get; set; }
		public string? segundo_apellido { get; set; }
		public string? tercer_apellido { get; set; }
		public string? sexo { get; set; }
		public string? fecha_nacimiento { get; set; }
		public string? direccion { get; set; }
		public string? descripcion_direccion { get; set; }
		public string? celular { get; set; }
		public string? telefono_domicilio { get; set; }
		public string? correo { get; set; }
		public string? numero_documento_identificacion { get; set; }
		public long nsec_documento_identificacion { get; set; }
		public long nsec_estado_civil { get; set; }
		public long nsec_usuario_creacion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }
		public string? codigo_funcionario { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
