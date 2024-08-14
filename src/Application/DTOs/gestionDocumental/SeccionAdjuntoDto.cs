using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
    public class SeccionAdjuntoDto
    {
        public long num_sec { get; set; }
        public int nro_orden_seccion { get; set; }
        public string valor { get; set; } = string.Empty;
        public long nsec_volumen { get; set; }
        public string? nombre_archivo { get; set; }
        public string? tamano_archivo { get; set; }
        public string? extension_archivo { get; set; }
       
        [JsonIgnore]
        public string? estado { get; set; }
        public long foja { get; }

        [JsonIgnore]
        public int total { get; set; }
    }
}
