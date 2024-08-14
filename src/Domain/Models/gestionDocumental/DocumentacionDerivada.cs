using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class DocumentacionDerivada
    {
        public long num_sec { get; set; }
        public long nsec_documento { get; set; }
        public string? fecha_derivacion { get; set; }
        public string? nota { get; set; }
        public int dias_plazo { get; set; }
		public string? observacion { get; set; }
        public string? estado { get; set; }
        public long nsec_area_origen { get; set; }
        public long nsec_area_destino { get; set; }
        public string? nombre_area_origen { get; set; }
        public string? nombre_area_destino { get; set; }

        [JsonIgnore]
        
        public long nsec_usuario_registro { get; set; }
    }
}
