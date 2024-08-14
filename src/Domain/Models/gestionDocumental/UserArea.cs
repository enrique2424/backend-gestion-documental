using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class UserArea
    {
        public string? num_sec { get; set; }

        public long? nsec_area { get; set; }
        public long? nsec_usuario { get; set; }

        // public long nsec_usuario_registro { get; set; }
        //[JsonIgnore]
        //public DateTime? fecha_registro { get; set; }

        public string? descripcion { get; set; }
        [JsonIgnore]
        public string? estado { get; set; }


    }
}
