using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Area
    {
        public string? num_sec { get; set; }
        public string? dependencia { get; set; }
        public string? codigo { get; set; }
        public string? descripcion { get; set; }
       
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
