
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
    public class EstadoPrestamoController : ControllerBase
    {
        private readonly IEstadoPrestamoService _estadoPrestamoService;

        public EstadoPrestamoController(IEstadoPrestamoService estadoPrestamoService)
        {
            _estadoPrestamoService = estadoPrestamoService;
        }

        // GET: api/EstadoPrestamo
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _estadoPrestamoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/EstadoPrestamo/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _estadoPrestamoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/EstadoPrestamo
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] EstadoPrestamo estadoPrestamo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            estadoPrestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoPrestamoService.Guardar(estadoPrestamo);
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

        // PUT api/EstadoPrestamo
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] EstadoPrestamo estadoPrestamo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            estadoPrestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoPrestamoService.Modificar(estadoPrestamo);
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


        // DELETE api/EstadoPrestamo/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var estadoPrestamo = await _estadoPrestamoService.BuscarPorNumSec(codigo);
            estadoPrestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _estadoPrestamoService.Eliminar(estadoPrestamo);
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

