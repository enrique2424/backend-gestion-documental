using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class UserAreaDto
    {
		public string? num_sec { get; set; }
        public long? nsec_area { get; set; }

        public string area { get; set; }
        public long? nsec_usuario { get; set; }

        public string? nombreusuario { get; set; }

        // public long nsec_usuario_registro { get; set; }
        //[JsonIgnore]
        //public DateTime? fecha_registro { get; set; }

        public string? descripcion { get; set; }
 
        public string? estado { get; set; }
        [JsonIgnore]
        public int total { get; set; }
    }
}
