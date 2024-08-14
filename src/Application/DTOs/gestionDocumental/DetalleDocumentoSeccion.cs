using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
    public class DetalleDocumentoSeccionDto
    {
        public long nsec_documento { get; set; }
        public string? observacion { get; set; }
        public long nsec_tomo { get; set; }
        public string? tomo { get; set; }
        public string? volumen { get; set; }
        public long nsec_seccion { get; set; }
        public string? descripcion { get; set; }
        public long nsec_filtro { get; set; }
        public long nsec_adjunto { get; set; }
        public string? valor { get; set; }

        
       
    }
}