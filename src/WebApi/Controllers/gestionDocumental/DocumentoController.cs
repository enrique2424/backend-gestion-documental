
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
using System.Drawing;
using System.Reflection.Metadata;
using Application.Services.GestionDocumental;

namespace WebApi.Controllers.GestionDocumental
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DocumentoController : ControllerBase
    {
        private readonly IDocumentoService _documentoService;

        public DocumentoController(IDocumentoService documentoService)
        {
            _documentoService = documentoService;
        }

        // GET: api/Documento
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _documentoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Documento/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _documentoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Documento
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] Documento documento)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            documento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _documentoService.Guardar(documento);
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

        // PUT api/Documento
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] Documento documento)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            documento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _documentoService.Modificar(documento);
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


        // DELETE api/Documento/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var documento = await _documentoService.BuscarPorNumSec(codigo);
            documento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _documentoService.Eliminar(documento);
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

        // GET: api/Documento
        [HttpGet("TraerdetalleDocumento/{nsec_documento}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> TraerDetalleDocumento(string nsec_documento)
        {
            
            var datos = await _documentoService.TraerdetalleDocumento(nsec_documento);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }


        // POST api/Documento
        [HttpPost("BuscarDocumento")]
        
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> BuscarDocumento([FromBody] BusquedaDocumento parametros)
        {
            // string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            // documento.nsec_usuario_registro = long.Parse(nsecUsuario);

            var respuestaListado = await _documentoService.BuscarDocumento(parametros);
            return Ok(respuestaListado);
        }


        [HttpGet("detalle/{nsec_documento}/seccion")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> DetalleDocumentoSeccion(string nsec_documento)
        {

            var datos  = await _documentoService.BuscarDetalleSeccion(long.Parse(nsec_documento));
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }


        [HttpGet("BuscarListadoxArea")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar, string nsec_area)
        {
            var respuestaListado = await _documentoService.BuscarListadoxArea(valor, parametro, numeroPagina, cantidadMostrar,nsec_area);
            return Ok(respuestaListado);
        }
    }

}

