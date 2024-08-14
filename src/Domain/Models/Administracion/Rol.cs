using System.Text.Json.Serialization;

namespace Domain.Models.Administracion
{
    public class Rol
    {
        [JsonIgnore]
        public long num_sec { get; set; }
        public string descripcion { get; set; } = "";

    }
}
