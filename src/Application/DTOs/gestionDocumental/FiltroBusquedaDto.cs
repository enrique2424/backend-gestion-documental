using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class FiltroBusquedaDto
    {
		public long nsec_filtro { get; set; }
		public long nsec_identificador { get; set; }
		public string? filtro { get; set; }
		public string? identificador { get; set; }		
		public string? tipo_dato { get; set; }
		public Boolean obligatorio { get; set; }

		[JsonIgnore]
        public int total { get; set; }
    }
}
