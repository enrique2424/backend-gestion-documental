using Microsoft.AspNetCore.Http;


namespace Application.Utils
{
    public interface IFichero
    {
        public string CrearCarpeta(string rutaRaiz, string nombreCarpeta);

        public Task<bool> GuadarArchivoAsync(string rutaCarpeta, string nombreArchivo, IFormFile file);

        public string ObtenerExtensionArchivo(string nombreArchivo);

        public Task<byte[]> ObtenerArchivoAsync(string rutaArchivo);
    }
}
