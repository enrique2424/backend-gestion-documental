using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class RolDto
    {
        public string? num_sec { get; set; }
        public string? descripcion { get; set; }
        [JsonIgnore]
        public int total { get; set; }
    }
}
