using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.GestionDocumental
{
    public class BusquedaDocumento
    {
        public long? nsec_secretaria { get; set; }
        public long? nsec_gestion { get; set; }
        public long? nsec_tipo_documento { get; set; }
        public long? codigo { get; set; }
        public long? nsec_filtro_busqueda { get; set; }
        public string? valor_busqueda { get; set; }
        public int? numeropaginaactual { get; set; }
        public int? cantidadmostrar { get; set; }
    }
}
