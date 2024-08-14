using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class DetSeccionFiltroBusqueda
    {
        public string? num_sec { get; set; }
		public string? nsec_seccion { get; set; }
		public string? nsec_filtro_busqueda { get; set; }
		public string? descripcion { get; set; }
		public string? valor { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }
}
