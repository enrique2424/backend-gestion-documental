
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
    public class DetPrestamoController : ControllerBase
    {
        private readonly IDetPrestamoService _detPrestamoService;

        public DetPrestamoController(IDetPrestamoService detPrestamoService)
        {
            _detPrestamoService = detPrestamoService;
        }

        // GET: api/DetPrestamo
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _detPrestamoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }


        // GET api/DetPrestamo/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _detPrestamoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }


        // POST api/DetPrestamo
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] DetPrestamo detPrestamo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detPrestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detPrestamoService.Guardar(detPrestamo);
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

        // PUT api/DetPrestamo
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] DetPrestamo detPrestamo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detPrestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detPrestamoService.Modificar(detPrestamo);
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


        // DELETE api/DetPrestamo/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detPrestamo = await _detPrestamoService.BuscarPorNumSec(codigo);
            detPrestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detPrestamoService.Eliminar(detPrestamo);
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

