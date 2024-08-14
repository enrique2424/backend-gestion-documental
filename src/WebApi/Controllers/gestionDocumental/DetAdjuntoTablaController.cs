
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
    public class DetAdjuntoTablaController : ControllerBase
    {
        private readonly IDetAdjuntoTablaService _detAdjuntoTablaService;

        public DetAdjuntoTablaController(IDetAdjuntoTablaService detAdjuntoTablaService)
        {
            _detAdjuntoTablaService = detAdjuntoTablaService;
        }

        // GET: api/DetAdjuntoTabla
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _detAdjuntoTablaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/DetAdjuntoTabla/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _detAdjuntoTablaService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/DetAdjuntoTabla
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] DetAdjuntoTabla detAdjuntoTabla)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detAdjuntoTabla.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detAdjuntoTablaService.Guardar(detAdjuntoTabla);
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

        // PUT api/DetAdjuntoTabla
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] DetAdjuntoTabla detAdjuntoTabla)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            detAdjuntoTabla.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detAdjuntoTablaService.Modificar(detAdjuntoTabla);
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


        // DELETE api/DetAdjuntoTabla/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detAdjuntoTabla = await _detAdjuntoTablaService.BuscarPorNumSec(codigo);
            detAdjuntoTabla.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _detAdjuntoTablaService.Eliminar(detAdjuntoTabla);
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


        // GET api/DetArchivoFormulario/5/det_archivos
        [HttpGet("{nsec_seccion}/det_archivos")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> ObtenerArchivos(int nsec_seccion)
        {
            var datos = await _detAdjuntoTablaService.BuscarArchivos(nsec_seccion);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

    }
}

