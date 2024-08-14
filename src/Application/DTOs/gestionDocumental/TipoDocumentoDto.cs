using Domain.Models.GestionDocumental;
using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class TipoDocumentoDto
    {
		public long num_sec { get; set; }
		public string? descripcion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }

    public class TipoDocumentoMasterDto
    {
        public TipoDocumento? tipoDocumento { get; set; }
        public List<FiltroBusqueda>? filtroBusquedas { get; set; }
    }
}
