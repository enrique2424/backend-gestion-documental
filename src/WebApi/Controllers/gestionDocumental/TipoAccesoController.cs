
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
    public class TipoAccesoController : ControllerBase
    {
        private readonly ITipoAccesoService _tipoAccesoService;

        public TipoAccesoController(ITipoAccesoService tipoAccesoService)
        {
            _tipoAccesoService = tipoAccesoService;
        }

        // GET: api/TipoAcceso
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _tipoAccesoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/TipoAcceso/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _tipoAccesoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/TipoAcceso
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] TipoAcceso tipoAcceso)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tipoAcceso.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoAccesoService.Guardar(tipoAcceso);
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

        // PUT api/TipoAcceso
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] TipoAcceso tipoAcceso)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tipoAcceso.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoAccesoService.Modificar(tipoAcceso);
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


        // DELETE api/TipoAcceso/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tipoAcceso = await _tipoAccesoService.BuscarPorNumSec(codigo);
            tipoAcceso.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoAccesoService.Eliminar(tipoAcceso);
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

