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
    public class SgEstadoCivilController : ControllerBase
    {
        private readonly ISgEstadoCivilService _sgEstadoCivilService;
        public SgEstadoCivilController(ISgEstadoCivilService sgEstadoCivilService)
        {
            _sgEstadoCivilService = sgEstadoCivilService;
        }
        // GET: api/SgEstadoCivil
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _sgEstadoCivilService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/SgEstadoCivil/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult<SgEstadoCivil>> Get(string codigo)
        {
            var datos = await _sgEstadoCivilService.BuscarPorNumSec(codigo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }
        // POST api/SgEstadoCivil
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] SgEstadoCivil sgEstadoCivil)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            sgEstadoCivil.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _sgEstadoCivilService.Guardar(sgEstadoCivil);
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

        // PUT api/SgEstadoCivil
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put(string codigo, [FromBody]  SgEstadoCivil sgEstadoCivil)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            sgEstadoCivil.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _sgEstadoCivilService.Modificar(long.Parse(codigo), sgEstadoCivil);
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
        // DELETE api/SgEstadoCivil/5
        //[HttpDelete("{codigo}")]
        //[Authorize(Roles = Roles.Administrador)]
        //public async Task<ActionResult> Delete(long codigo)
        //{
        //    string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
        //    var sgEstadoCivil = await _sgEstadoCivilService.BuscarPorNumSec(codigo);
        //    sgEstadoCivil.nsec_usuario_registro = long.Parse(nsecUsuario);
        //    var respuestaBD = await _sgEstadoCivilService.Eliminar(codigo);
        //    if (respuestaBD.status == Status.Error)
        //    {
        //        var respuestaError = new RespuestaError()
        //        {
        //            error = respuestaBD.status,
        //            message = respuestaBD.response
        //        };
        //        return BadRequest(respuestaError);
        //    }
        //    return Ok(respuestaBD);
        //}



    }
}

