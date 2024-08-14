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
    public class SgPersonaController : ControllerBase
    {
        private readonly ISgPersonaService _sgPersonaService;

        public SgPersonaController(ISgPersonaService sgPersonaService)
        {
            _sgPersonaService = sgPersonaService;
        }

        // GET: api/SgPersona
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _sgPersonaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/SgPersona/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _sgPersonaService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/SgPersona
        [HttpPost]
    
        public async Task<ActionResult> Post([FromBody] SgPersona sgPersona)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            sgPersona.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _sgPersonaService.Guardar(sgPersona);
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

        // PUT api/SgPersona
        [HttpPut("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put(string codigo,[FromBody] SgPersona sgPersona)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            sgPersona.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _sgPersonaService.Modificar(long.Parse(codigo),sgPersona);
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


        //DELETE api/SgPersona/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var sgPersona = await _sgPersonaService.BuscarPorNumSec(codigo);
            sgPersona.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _sgPersonaService.Eliminar(codigo);
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

