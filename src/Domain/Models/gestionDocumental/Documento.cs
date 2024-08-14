using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Documento
	{
		public string? num_sec { get; set; }
		public long nsec_tipo_documento { get; set; }
		public long nsec_estado_documento { get; set; }
		public long nsec_tipo_acceso { get; set; }
		public string? nsec_secretaria { get; set; }
		public string? nsec_area { get; set; }
		public long nsec_gestion { get; set; }
		public long nsec_fila { get; set; }
		public string? codigo { get; set; }
		public string? observacion { get; set; }
		[JsonIgnore]
		public string? estado { get; set; }
		public long nsec_usuario_registro { get; set; }
	}
}


