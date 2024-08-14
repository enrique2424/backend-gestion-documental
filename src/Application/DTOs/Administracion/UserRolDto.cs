using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Administracion
{
    public class UserRolDto
    {
        public string? num_sec { get; set; }
        public string? rol { get; set; }
        public string? roldescripcion { get; set; }
        public string? usuario { get; set; }
        [JsonIgnore]
        public int total { get; set; }
    }
}
