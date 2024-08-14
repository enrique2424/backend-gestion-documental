using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DocumentoDto
    {
		public long num_sec { get; set; }
		//public long nsec_tipo_documento { get; set; }
		//public long nsec_estado_documento { get; set; }
		//public long nsec_tipo_acceso { get; set; }
		//public long nsec_secretaria { get; set; }
		//public long nsec_area { get; set; }
		//public long nsec_gestion { get; set; }
		//public long nsec_fila { get; set; }
		public string? codigo { get; set; }
        public string? area_ { get; set; }
        public string? tipo_documento { get; set; }
        public string? gestion { get; set; }
        public string? estante { get; set; }
        public string? fila { get; set; }

        [JsonIgnore]
		public string? estado { get; set; }
		//public string? observacion { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
    public class DocumentoConEstadoDto
    {
        public long num_sec { get; set; }
        //public long nsec_tipo_documento { get; set; }
        //public long nsec_estado_documento { get; set; }
        //public long nsec_tipo_acceso { get; set; }
        //public long nsec_secretaria { get; set; }
        //public long nsec_area { get; set; }
        //public long nsec_gestion { get; set; }
        //public long nsec_fila { get; set; }
        public string? codigo { get; set; }
        public string? area_ { get; set; }
        public string? tipo_documento { get; set; }
        public string? gestion { get; set; }
        public string? estante { get; set; }
        public string? fila { get; set; }
        public long? nsec_estado { get; set; }
        [JsonIgnore]
        public string? estado { get; set; }
        //public string? observacion { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
