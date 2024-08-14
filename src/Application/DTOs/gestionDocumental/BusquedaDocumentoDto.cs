using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class BusquedaDocumentoDto
    {
		public long num_sec { get; set; }
		public string? codigo { get; set; }
        public string? secretaria { get; set; }
        public string? categoria_programatica { get; set; }
        public string? gestion { get; set; }
        public string? estante { get; set; }
        public string? fila { get; set; }
        public string? tipo_documento { get; set; }
        public string? estado_documento { get; set; }
        public string? observacion_documento { get; set; }
        public string? tomo { get; set; }
        public string? volumen { get; set; }
        public long? nsec_seccion { get; set; }
        public int? nro_orden_seccion { get; set; }
        public string? filtro_busqueda { get; set; }
        public string? valor { get; set; }
        public string? nombre_adjunto { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
