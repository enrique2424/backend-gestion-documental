using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Seccion
    {
        public string? num_sec { get; set; }
		public int nro_orden_seccion { get; set; }
        public string? nsec_volumen { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
