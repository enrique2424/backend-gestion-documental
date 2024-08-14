
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
    public class TomoController : ControllerBase
    {
        private readonly ITomoService _tomoService;

        public TomoController(ITomoService tomoService)
        {
            _tomoService = tomoService;
        }

        // GET: api/Tomo
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _tomoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Tomo/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _tomoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Tomo
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] Tomo tomo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tomo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tomoService.Guardar(tomo);
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

        // PUT api/Tomo
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] Tomo tomo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            tomo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tomoService.Modificar(tomo);
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


        // DELETE api/Tomo/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tomo = await _tomoService.BuscarPorNumSec(codigo);
            tomo.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tomoService.Eliminar(tomo);
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

