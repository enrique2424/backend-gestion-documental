using System.Text.Json.Serialization;

namespace Domain.Models.GestionDocumental
{
	public class DetPrestamo
    {
        public long num_sec { get; set; }
		public long nsec_prestamo { get; set; }
		public long nsec_documento { get; set; }
		public long estado_devolucion { get; set; }
        [JsonIgnore]
		public string? estado { get; set; }

        public long nsec_usuario_registro { get; set; }
    }

    public class traerDetallePorPrestamoDto
    {
        public long num_sec { get; set; }
        public long nsec_prestamo { get; set; }
        public long nsec_documento { get; set; }
        public long estado_devolucion { get; set; }
        
        [JsonIgnore]
        public string? estado { get; set; }
        public string cod_documento { get; set; }

    }
}
