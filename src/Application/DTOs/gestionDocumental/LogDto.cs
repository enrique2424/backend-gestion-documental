using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class LogDto
    {
		public long nsec_usuario { get; set; }
		public long num_sec { get; set; }
		public string? fecha { get; set; }
		public long id { get; set; }
		public int tipo_accion { get; set; }
		public string? datos { get; set; }
		public string? nombre_tabla { get; set; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
