
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
    public class VolumenController : ControllerBase
    {
        private readonly IVolumenService _volumenService;

        public VolumenController(IVolumenService volumenService)
        {
            _volumenService = volumenService;
        }

        // GET: api/Volumen
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _volumenService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Volumen/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _volumenService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Volumen
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] Volumen volumen)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            volumen.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _volumenService.Guardar(volumen);
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

        // PUT api/Volumen
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] Volumen volumen)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            volumen.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _volumenService.Modificar(volumen);
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


        // DELETE api/Volumen/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var volumen = await _volumenService.BuscarPorNumSec(codigo);
            volumen.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _volumenService.Eliminar(volumen);
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

