
using Application.Interfaces.IServices.GestionDocumental;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models;
using Domain.Models.GestionDocumental;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Drawing;
using System.Reflection.Metadata;

namespace WebApi.Controllers.GestionDocumental
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DocumentacionDerivadaController : ControllerBase
    {
        private readonly IDocumentacionDerivadaService _documentacionDerivadaService;

        public DocumentacionDerivadaController(IDocumentacionDerivadaService documentacionDerivadaService)
        {
            _documentacionDerivadaService = documentacionDerivadaService;
        }

        // GET: api/DocumentacionDerivada
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, int nsec_area_destino)
        {
            var respuestaListado = await _documentacionDerivadaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar, nsec_area_destino);
            return Ok(respuestaListado);
        }

        // GET api/DocumentacionDerivada/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _documentacionDerivadaService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/DocumentacionDerivada
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] DocumentacionDerivada documentacionDerivada)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            documentacionDerivada.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _documentacionDerivadaService.Guardar(documentacionDerivada);
            
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return Ok(respuestaBD);
        }

        // PUT api/DocumentacionDerivada
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] DocumentacionDerivada documentacionDerivada)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            documentacionDerivada.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _documentacionDerivadaService.Modificar(documentacionDerivada);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return Ok(respuestaBD);
        }


        // DELETE api/SgaDocumentacionDerivada/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var documentacionDerivada = await _documentacionDerivadaService.BuscarPorNumSec(codigo);
            documentacionDerivada.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _documentacionDerivadaService.Eliminar(documentacionDerivada);
            if (respuestaBD.status == Status.Error)
            {
                var respuestaError = new RespuestaError()
                {
                    error = respuestaBD.status,
                    message = respuestaBD.response
                };
                return BadRequest(respuestaError);
            }
            return Ok(respuestaBD);
        }

    }
}

