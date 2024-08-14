
namespace Application.DTOs.Reports
{
    public class PostParametrosReporte
    {

        public string? nombreReporte { get; set; }
        public ParametroReporte[]? arrayParametros { get; set; }
    }
    public class ParametroReporte
    {
        public string? nombre { get; set; }
        public string? valor { get; set; }
        public string? tipoDato { get; set; }
    }

}
