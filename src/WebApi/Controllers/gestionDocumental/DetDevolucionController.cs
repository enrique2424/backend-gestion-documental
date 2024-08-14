
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
    public class DetDevolucionController : ControllerBase
    {
        private readonly IDetDevolucionService _detDevolucionService;

        public DetDevolucionController(IDetDevolucionService detDevolucionService)
        {
            _detDevolucionService = detDevolucionService;
        }

        // GET: api/DetDevolucion
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _detDevolucionService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/DetDevolucion/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _detDevolucionService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/DetDevolucion
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] DetDevolucion detDevolucion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detDevolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detDevolucionService.Guardar(detDevolucion);
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

        // PUT api/DetDevolucion
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] DetDevolucion detDevolucion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detDevolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detDevolucionService.Modificar(detDevolucion);
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


        // DELETE api/DetDevolucion/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detDevolucion = await _detDevolucionService.BuscarPorNumSec(codigo);
            detDevolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detDevolucionService.Eliminar(detDevolucion);
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

