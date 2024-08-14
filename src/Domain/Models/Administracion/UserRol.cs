using System.Text.Json.Serialization;

namespace Domain.Models.Administracion
{
    public class UserRol
    {
       
        public long num_sec { get; set; }
        public long nsec_usuario { get; set; }
        public long nsec_rol { get; set; }
        public long nsec_aplicacion { get; set; } = 6;

     
    }
}
