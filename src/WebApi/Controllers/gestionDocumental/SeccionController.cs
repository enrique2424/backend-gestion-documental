
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
    public class SeccionController : ControllerBase
    {
        private readonly ISeccionService _seccionService;
        private readonly IDetSeccionFiltroBusquedaService _detalleSeccionService;

        public SeccionController(ISeccionService seccionService, IDetSeccionFiltroBusquedaService detalleSeccionService)
        {
            _seccionService = seccionService;
            _detalleSeccionService= detalleSeccionService;
        }

        // GET: api/Seccion
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _seccionService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Seccion/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _seccionService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Seccion
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] Seccion seccion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            seccion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _seccionService.Guardar(seccion);
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

        // PUT api/Seccion
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] Seccion seccion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            seccion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _seccionService.Modificar(seccion);
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


        // DELETE api/Seccion/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var seccion = await _seccionService.BuscarPorNumSec(codigo);
            seccion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _seccionService.Eliminar(seccion);
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

