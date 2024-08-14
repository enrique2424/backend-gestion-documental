using Application.DTOs.Reports;
using Application.Interfaces.IRepositories.Reports;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using Domain.Models.Data;
using Domain.Enums;
using System.Net;
using Microsoft.Extensions.Configuration;
using Application.Utils;

namespace Infrastructure.Repositories.Reports
{
    public class JasperReportsRepository : IJasperReportsRepository
    {
        private IConfiguration _appSettingsInstance;
        public JasperReportsRepository(IConfiguration appSettingsInstance)
        {
            _appSettingsInstance = appSettingsInstance;
            //_appSettingsInstance = new ConfigurationBuilder()
            //                        .AddJsonFile("appsettings.json").Build();
        }

        public async Task<RespuestaCore> traerReportePDF(PostParametrosReporte objParametros, string extension = "pdf")
        {
            try
            {
                byte[] bytesResponseData;
                RespuestaCore auxRespuesta = new RespuestaCore();



                string parametrosArmado = Application.Utils.MetodosGlobales.concatenarParametros(objParametros.arrayParametros);
                using (HttpClient client = new HttpClient())
                {
                    string jasperUrl = _appSettingsInstance.GetConnectionString("JasperUrl");
                    string jasperUser = _appSettingsInstance.GetConnectionString("JasperUser");
                    string jasperPassword = _appSettingsInstance.GetConnectionString("JasperPassword");
                    string url = jasperUrl + objParametros.nombreReporte + $".{extension}?j_username={jasperUser}&j_password={jasperPassword}" + parametrosArmado;
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    bytesResponseData = await response.Content.ReadAsByteArrayAsync();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (response.Content.Headers.ContentLength <= JasperReportObject.VALOR_BLANCO_PDF)
                        {
                            return new RespuestaCore
                            {
                                status = Status.Error
                                ,
                                response = "No hay resultados para mostrar"
                            };
                        }

                        return new RespuestaCore
                        {
                            status = Status.Success
                            ,
                            response = bytesResponseData
                        };
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return new RespuestaCore
                        {
                            status = Status.Error
                            ,
                            response = "Error de conexión con el servidor de reportes"
                        };
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return new RespuestaCore
                        {
                            status = Status.Error
                            ,
                            response = "Hay problemas con el reporte solicitado, intente más tarde"
                        };
                    }
                }
                return auxRespuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}

