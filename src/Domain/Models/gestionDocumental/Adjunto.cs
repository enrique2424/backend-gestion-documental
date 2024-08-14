using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class Adjunto
    {
        public long num_sec { get; set; }
        public string? nombre { get; set; }
        public string? nombre_fisico { get; set; }
        public long tamano { get; set; }
        public string? content_type { get; set; }       
       
		public string? estado { get; set; }	

        public string? nsec_volumen { get; set; }
        public long? foja { get; set; }
        public long? nsec_seccion { get; set;}
        public long nsec_usuario_registro { get; set; }
       
    }
    public class PostArchivo
    {
        //public int tamano { get; set; }
        public long nsec_usuario { get; set; }    
    
        public string? nsec_volumen { get; set; }
        public string? nombre { get; set; }
        public List<IFormFile>? archivo { get; set; }
        public long foja { get; set; }
        public long? nsec_seccion { get; set; }
        public List<DetSeccionFiltroBusqueda> arrayFiltroBusqueda { get; set; } = new List<DetSeccionFiltroBusqueda>();
    }
    public class PutArchivo
    {
        public long nsec_usuario { get; set; }
        public long num_sec { get; set; }
        public string? nombre { get; set; }
        public string? nsec_volumen { get; set; }
       
        public List<IFormFile>? archivo { get; set; }
        public long foja { get; set; }
        public long? nsec_seccion { get; set; }
        public List<DetSeccionFiltroBusqueda> arrayFiltroBusqueda { get; set; } = new List<DetSeccionFiltroBusqueda>();
    }
    public class ObjArchivo
    {
        public Adjunto? objArchivo { get; set; }

        public IFormFile? file { get; set; }
    }
    public class ArchivoListado
    {
        public long num_sec { get; set; }
        public string? nombre { get; set; }
        public int tamano { get; set; }
        public string? content_type { get; set; }

        public int total { get; set; }
    }
}
