
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

namespace WebApi.Controllers.GestionDocumental
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class EstadoDocumentoController : ControllerBase
    {
        private readonly IEstadoDocumentoService _estadoDocumentoService;

        public EstadoDocumentoController(IEstadoDocumentoService estadoDocumentoService)
        {
            _estadoDocumentoService = estadoDocumentoService;
        }

        // GET: api/EstadoDocumento
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _estadoDocumentoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/EstadoDocumento/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _estadoDocumentoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/EstadoDocumento
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] EstadoDocumento estadoDocumento)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            estadoDocumento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoDocumentoService.Guardar(estadoDocumento);
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

        // PUT api/EstadoDocumento
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] EstadoDocumento estadoDocumento)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            estadoDocumento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoDocumentoService.Modificar(estadoDocumento);
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


        // DELETE api/EstadoDocumento/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var estadoDocumento = await _estadoDocumentoService.BuscarPorNumSec(codigo);
            estadoDocumento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoDocumentoService.Eliminar(estadoDocumento);
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

