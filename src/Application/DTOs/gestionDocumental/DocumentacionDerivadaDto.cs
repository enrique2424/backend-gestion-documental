using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DocumentacionDerivadaDto
    {
        public long num_sec { get; set; }
        public string? codigo { get; set; }
        public long nsec_documento { get; set; }
        public DateTime? fecha_derivacion { get; set; }
        public string? referencia { get; set; }
        public long? nsec_estado { get; set; }
        public string? descripcion { get; set; }
        public long? nsec_area_destino { get; set; }
        public string? nombre_area_origen { get; set; }

        [JsonIgnore]
        public string? estado { get; set; }
        public int total { get; set; }  
    }
}
