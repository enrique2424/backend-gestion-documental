
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
using HeyRed.Mime;
using Application.Services.gestionDocumental;
using Domain.Models.gestionDocumental;
using Application.Services.GestionDocumental;

namespace WebApi.Controllers.GestionDocumental
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AdjuntoController : ControllerBase
    {
        private readonly IAdjuntoService _adjuntoService;
        private readonly ArchivoMongoService _archivoMongo;
        


        public AdjuntoController(IAdjuntoService adjuntoService, ArchivoMongoService archivoMongo)
        {
            _adjuntoService = adjuntoService;
            _archivoMongo = archivoMongo;           
        }

        // GET: api/Adjunto
        [HttpGet]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _adjuntoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Adjunto/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _adjuntoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Adjunto
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Post([FromBody] Adjunto adjunto)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            adjunto.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _adjuntoService.Guardar(adjunto);
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

        // POST api/Archivo
        [HttpPost("mongo")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> GuardarMongo([FromForm] PostArchivo archivo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            archivo.nsec_usuario = long.Parse(nsecUsuario);  
            var respuestaBD = await _adjuntoService.PostArchivoMongo(archivo);
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
            //return null;
        }
        [HttpPost("mongoUpdate")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> ModificarMongo([FromForm] PostArchivo archivo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            archivo.nsec_usuario = long.Parse(nsecUsuario);
            var respuestaBD = await _adjuntoService.PutArchivoMongo(archivo);
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
            //return null;
        }
        [HttpGet("traer_archivo/{num_sec}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> TraerArchivo(string num_sec)
        {
            object response = new object();
            try
            {


                byte[] datosByte = await this._archivoMongo.Get(num_sec);

                if (datosByte != null)
                {
                    var memory = new MemoryStream(datosByte);
                    string mimeType = MimeTypesMap.GetMimeType(num_sec);
                    return File(memory, mimeType, num_sec);

                }
                else
                {
                    return  BadRequest(new
                    {
                        status = "empty",
                        response = datosByte
                    });
                }
            }
            catch (Exception ex)
            {
                response = new
                {
                    status = "error",
                    response = "No se cargo el archivo"
                };

                return  BadRequest(response);
            }
        }

        // PUT api/Adjunto
        [HttpPut]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Put([FromBody] Adjunto adjunto)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            adjunto.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _adjuntoService.Modificar(adjunto);
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


        // DELETE api/Adjunto/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var adjunto = await _adjuntoService.BuscarPorNumSec(codigo);
            adjunto.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _adjuntoService.Eliminar(adjunto);
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

