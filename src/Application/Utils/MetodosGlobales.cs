﻿using Application.DTOs.Reports;
using Microsoft.AspNetCore.Http;

namespace Application.Utils
{
    public static class MetodosGlobales
    {
        public static string concatenarParametros(ParametroReporte[]? parametros)
        {
            string parametrosRet = "";

            for (int i = 0; i < parametros!.Length; i++)
            {
                ParametroReporte parametroActual = parametros[i];

                parametrosRet = $"{parametrosRet}&{parametroActual.nombre}={parametroActual.valor}";
            }

            // for (string[] parametro in parametros)
            // {
            //     parametrosRet = $"{parametro[0]}={parametro[1]}&";
            // }

            return parametrosRet;
        }

        public static byte[] fileToArrayByte(IFormFile file)
        {
            byte[] fileByte = null;
            if (file != null)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                fileByte = ms.ToArray();
            }
            return fileByte;
        }
    }
}
