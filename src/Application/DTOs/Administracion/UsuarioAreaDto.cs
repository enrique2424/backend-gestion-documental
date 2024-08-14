using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class UsuarioAreaDto
    {
        public string? num_sec { get; set; }
        public string? nombre { get; set; }
        [JsonIgnore]
        public string? cuenta { get; set; }
        [JsonIgnore]
        public string? contrasena { get; set; }
        [JsonIgnore]
        public string? tipo { get; set; }
        [JsonIgnore]

        public string? genero { get; set; }
        [JsonIgnore]

        public string? imagenbyte { get; set; }
        [JsonIgnore]

        public string? contentype { get; set; }
        [JsonIgnore]

        public string? nsec_rol { get; set; }
        [JsonIgnore]

        public string? nombre_rol { get; set; }
        [JsonIgnore]

        public int total { get; set; }
    }
}
