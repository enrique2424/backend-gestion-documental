
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
    public class DetSeccionFiltroBusquedaController : ControllerBase
    {
        private readonly IDetSeccionFiltroBusquedaService _detSeccionFiltroBusquedaService;

        public DetSeccionFiltroBusquedaController(IDetSeccionFiltroBusquedaService detSeccionFiltroBusquedaService)
        {
            _detSeccionFiltroBusquedaService = detSeccionFiltroBusquedaService;
        }

        // GET: api/DetSeccionFiltroBusqueda
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _detSeccionFiltroBusquedaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/DetSeccionFiltroBusqueda/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _detSeccionFiltroBusquedaService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/DetSeccionFiltroBusqueda
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] DetSeccionFiltroBusqueda detSeccionFiltroBusqueda)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detSeccionFiltroBusqueda.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detSeccionFiltroBusquedaService.Guardar(detSeccionFiltroBusqueda);
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

        // PUT api/DetSeccionFiltroBusqueda
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] DetSeccionFiltroBusqueda detSeccionFiltroBusqueda)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detSeccionFiltroBusqueda.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detSeccionFiltroBusquedaService.Modificar(detSeccionFiltroBusqueda);
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


        // DELETE api/DetSeccionFiltroBusqueda/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detSeccionFiltroBusqueda = await _detSeccionFiltroBusquedaService.BuscarPorNumSec(codigo);
            //detSeccionFiltroBusqueda.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detSeccionFiltroBusquedaService.Eliminar(detSeccionFiltroBusqueda);
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

