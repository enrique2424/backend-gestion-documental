using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DetSeccionFiltroBusquedaDto
    {
		public long num_sec { get; set; }
		public long nsec_seccion { get; set; }
		public long nsec_filtro_busqueda { get; set; }
		public string? descripcion { get; set; }
		public string? valor { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
