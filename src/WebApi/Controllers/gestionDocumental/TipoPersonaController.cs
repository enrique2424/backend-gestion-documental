
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
    public class TipoPersonaController : ControllerBase
    {
        private readonly ITipoPersonaService _tipoPersonaService;

        public TipoPersonaController(ITipoPersonaService tipoPersonaService)
        {
            _tipoPersonaService = tipoPersonaService;
        }

        // GET: api/TipoPersona
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _tipoPersonaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/TipoPersona/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _tipoPersonaService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/TipoPersona
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] TipoPersona tipoPersona)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tipoPersona.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoPersonaService.Guardar(tipoPersona);
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

        // PUT api/TipoPersona
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] TipoPersona tipoPersona)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tipoPersona.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoPersonaService.Modificar(tipoPersona);
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


        // DELETE api/TipoPersona/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tipoPersona = await _tipoPersonaService.BuscarPorNumSec(codigo);
            tipoPersona.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoPersonaService.Eliminar(tipoPersona);
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

