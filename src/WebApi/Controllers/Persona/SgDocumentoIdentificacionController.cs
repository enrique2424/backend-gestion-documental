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
    public class SgDocumentoIdentificacionController : ControllerBase
    {
        private readonly ISgDocumentoIdentificacionService _sgDocumentoIdentificacionService;

        public SgDocumentoIdentificacionController(ISgDocumentoIdentificacionService sgDocumentoIdentificacionService)
        {
            _sgDocumentoIdentificacionService = sgDocumentoIdentificacionService;
        }

        // GET: api/SgDocumentoIdentificacion
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _sgDocumentoIdentificacionService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/SgDocumentoIdentificacion/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult<SgDocumentoIdentificacion>> Get(string codigo)
        {
            var datos = await _sgDocumentoIdentificacionService.BuscarPorNumSec(codigo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/SgDocumentoIdentificacion
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] SgDocumentoIdentificacion sgDocumentoIdentificacion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            sgDocumentoIdentificacion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _sgDocumentoIdentificacionService.Guardar(sgDocumentoIdentificacion);
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

        // PUT api/SgDocumentoIdentificacion
        //    [HttpPut]
        //    [Authorize(Roles = Roles.Administrador)]
        //    public async Task<ActionResult> Put([FromBody] SgDocumentoIdentificacion sgDocumentoIdentificacion)
        //    {
        //        string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
        //        sgDocumentoIdentificacion.nsec_usuario_registro = long.Parse(nsecUsuario);
        //        var respuestaBD = await _sgDocumentoIdentificacionService.Modificar(sgDocumentoIdentificacion);
        //        if (respuestaBD.status == Status.Error)
        //        {
        //            var respuestaError = new RespuestaError()
        //            {
        //                error = respuestaBD.status,
        //                message = respuestaBD.response
        //            };
        //            return BadRequest(respuestaError);
        //        }
        //        return Ok(respuestaBD);
        //    }
        //}

        //DELETE api/sgdocumentoidentificacion/5
    //    [httpDelete("{codigo}")]
    //    [authorize(roles = roles.administrador)]
    //    public async task<actionresult> delete(long codigo)
    //    {
    //        string nsecusuario = user!.findfirst(claimtypes.sid)!.value;
    //        var sgdocumentoidentificacion = await _sgdocumentoidentificacionservice.buscarpornumsec(codigo);
    //        sgdocumentoidentificacion.nsec_usuario_registro = long.parse(nsecusuario);
    //        var respuestabd = await _sgdocumentoidentificacionservice.eliminar(sgdocumentoidentificacion);
    //        if (respuestabd.status == status.error)
    //        {
    //            var respuestaerror = new respuestaerror()
    //            {
    //                error = respuestabd.status,
    //                message = respuestabd.response
    //            };
    //            return badrequest(respuestaerror);
    //        }
    //        return ok(respuestabd);
    //    }

    //}
}



