using Application.DTOs.Reports;
using Application.Interfaces.IRepositories.Reports;
using Application.Interfaces.IServices.Reports;
using Application.Utils;
using Domain.Models.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Reports
{
    [Route("api/[controller]")]
    [ApiController]
    public class JasperReportsController : ControllerBase
    {
        private readonly IJasperReportsService _jasperReportsService;

        public JasperReportsController(IJasperReportsService jasperReportsService)
        {
            _jasperReportsService = jasperReportsService;
        }

        // DELETE api/JasperReports/5
        [HttpPost]
        [Route("traerReportePDF")]
        // [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> traerReportePDF([FromBody] PostParametrosReporte objParametros)
        {
            // string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var respuestaBD = await _jasperReportsService.traerReportePDF(objParametros);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return File(respuestaBD.response, "application/pdf;", objParametros.nombreReporte);
        }

        

    }
}
