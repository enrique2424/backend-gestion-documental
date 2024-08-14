using System.Text.Json.Serialization;

namespace Application.DTOs.GestionDocumental
{
	public class DetalleDocumentoDto
	{

        //public List<DetalleTomoDto>? arrayDetTomo { get; set; }
        public List<TomoDto>? arrayTomo { get; set; }
        public List<VolumenDto>? arrayVolumen { get; set; }
        public List<SeccionAdjuntoDto>? arrarySeccion { get; set; }



    }
}
