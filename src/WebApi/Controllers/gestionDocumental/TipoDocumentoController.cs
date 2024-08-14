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
using Application.DTOs.GestionDocumental;

namespace WebApi.Controllers.GestionDocumental
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITipoDocumentoService _tipoDocumentoService;
        private readonly IFiltroBusquedaService _filtroBusquedaService;

        public TipoDocumentoController(ITipoDocumentoService tipoDocumentoService, IFiltroBusquedaService filtroBusquedaService)
        {
            _tipoDocumentoService = tipoDocumentoService;
            _filtroBusquedaService = filtroBusquedaService;

        }

        // GET: api/TipoDocumento
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _tipoDocumentoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/TipoDocumento/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _tipoDocumentoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/TipoDocumento
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] TipoDocumentoMasterDto tipoDocumentoMaster)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tipoDocumento = tipoDocumentoMaster.tipoDocumento;
            tipoDocumento!.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoDocumentoService.Guardar(tipoDocumento);
            foreach (var f in tipoDocumentoMaster.filtroBusquedas) {
                var filter = new FiltroBusqueda()
                {
                    descripcion = f.descripcion,
                    estado = f.estado,
                    nsec_tipo_documento = long.Parse(respuestaBD.numsec),
                    nsec_tipo_identificador = f.nsec_tipo_identificador,
                    nsec_usuario_registro = long.Parse(nsecUsuario),
                    num_sec = 0
                };
                var res = await _filtroBusquedaService.Guardar(filter);
            }
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

        // PUT api/TipoDocumento
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] TipoDocumentoMasterDto tipoDocumentoMaster)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tipoDocumento = tipoDocumentoMaster.tipoDocumento;
            tipoDocumento!.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoDocumentoService.Modificar(tipoDocumento);
            foreach (var f in tipoDocumentoMaster.filtroBusquedas)
            {
                var filter = new FiltroBusqueda()
                {
                    descripcion = f.descripcion,
                    estado = f.estado,
                    nsec_tipo_documento = long.Parse(respuestaBD.numsec),
                    nsec_tipo_identificador = f.nsec_tipo_identificador,
                    nsec_usuario_registro = long.Parse(nsecUsuario),
                    num_sec = f.num_sec
                };
                if (f.num_sec == 0) {
                    var filtroPorGuardar = await _filtroBusquedaService.Guardar(filter);
                }
                var res = await _filtroBusquedaService.Modificar(filter);
            }
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

       
        // DELETE api/TipoDocumento/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var tipoDocumento = await _tipoDocumentoService.BuscarPorNumSec(codigo);
            tipoDocumento.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _tipoDocumentoService.Eliminar(tipoDocumento);
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


        [HttpGet("TraerListaFiltro/{nsec_tipo_documento}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> TraerListaFiltroBusqueda(int nsec_tipo_documento)
        {
            long nsecUsuario = long.Parse(User!.FindFirst(ClaimTypes.Sid)!.Value);
            int nsecRol = int.Parse(User!.FindFirst(ClaimCustom.NsecRol)!.Value);

            var respuestaListado = await _filtroBusquedaService.TraerListaFiltro(nsec_tipo_documento);
            return Ok(respuestaListado);
        }


        [HttpGet("TraerDocumentoMaster/{nsec_tipo_documento}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> TraerDocumentoMaster(int nsec_tipo_documento)
        {
            long nsecUsuario = long.Parse(User!.FindFirst(ClaimTypes.Sid)!.Value);
            int nsecRol = int.Parse(User!.FindFirst(ClaimCustom.NsecRol)!.Value);

            var respuestaListado = await _tipoDocumentoService.TraerDocumentoMaster(nsec_tipo_documento);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = respuestaListado
            };
            return Ok(respuesta);
        }
    }

}

