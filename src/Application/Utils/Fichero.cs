using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Utils
{
    public class Fichero:IFichero
    {
        public Fichero()
        {

        }

        public string CrearCarpeta(string rutaRaiz, string nombreCarpeta)
        {
            string pathCarpeta = Path.Combine(rutaRaiz, nombreCarpeta);

            if (!Directory.Exists(pathCarpeta))
            {
                Directory.CreateDirectory(pathCarpeta);
            }

            return pathCarpeta;
        }

        /// <summary>
        /// Método para Guardar un Archivo en Disco
        /// </summary>
        /// <param name="rutaCarpeta">Ruta de la Carpeta donde se va Guardar.</param>
        /// <param name="nombreArchivo">Nombre del Archivo para Guardar en Disco.</param>
        /// <param name="file">Archivo a Guardar en Disco </param>
        /// <returns></returns>
        public async Task<bool> GuadarArchivoAsync(string rutaCarpeta, string nombreArchivo, IFormFile file)
        {
            try
            {
                bool resp = false;

                var fullPath = Path.Combine(rutaCarpeta, nombreArchivo);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await (file.CopyToAsync(stream));
                    resp = true;
                }

                return resp;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string ObtenerExtensionArchivo(string nombreArchivo)
        {
            string[] array = nombreArchivo.Split('.');
            int indice = array.Length - 1;

            string extension = array[indice];

            return extension;
        }


        /// <summary>
        /// Método para Leer un archivo de disco y Convertirlo en binario (byte[]).
        /// </summary>
        /// <param name="rutaArchivo">Ruta fisica del Archivo con su Extensión (.pdf, etc.).</param>
        /// <returns></returns>
        public async Task<byte[]> ObtenerArchivoAsync(string rutaArchivo)
        {
            try
            {
                FileStream fs = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                int len = (int)fs.Length;
                byte[] contenido = new byte[len];
                // fs.Read(contenido, 0, len);
                await fs.ReadAsync(contenido, 0, len);
                fs.Close();

                return contenido;

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
