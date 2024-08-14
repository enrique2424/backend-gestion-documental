using Application.Interfaces.IServices.Reports;
using Domain.Models;
using Application.DTOs.Reports;
//using Application.Interfaces.IServices.Tesoreria;
//using Domain.Models;
//using Domain.Models.Tesoreria;
//using Application.DTOs.Tesoreria;
using Application.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Models.Data;


namespace WebApi.Controllers.Reports
{
    [Route("api/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class JasperReportssControllers : ControllerBase
    {
        private readonly IJasperReportsService _jasperReportsService;

        public JasperReportssControllers(IJasperReportsService jasperReportsService)
        {
            _jasperReportsService = jasperReportsService;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*
        // DELETE api/JasperReports/5
        [HttpPost]
        [Route("traerReportePDF")]
        [Authorize(Roles = Roles.Todos)]
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

        // POST api/JasperReports
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Cortes cortes)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            cortes.nsec_usuario = long.Parse(nsecUsuario);
            var respuestaBD = new RespuestaDB(); //  await _cortesService.Guardar(cortes);
            //if (respuestaBD.status == Status.Error)
            //{
            //    var respuestaError = new RespuestaError()
            //    {
            //        error = respuestaBD.status,
            //        message = respuestaBD.response
            //    };
            //    return BadRequest(respuestaError);
            //}
            return Ok(respuestaBD);
        }

     
        */


    }
}
