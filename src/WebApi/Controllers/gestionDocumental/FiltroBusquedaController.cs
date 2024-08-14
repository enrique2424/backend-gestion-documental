
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
    public class FiltroBusquedaController : ControllerBase
    {
        private readonly IFiltroBusquedaService _filtroBusquedaService;

        public FiltroBusquedaController(IFiltroBusquedaService filtroBusquedaService)
        {
            _filtroBusquedaService = filtroBusquedaService;
        }

        // GET: api/FiltroBusqueda
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _filtroBusquedaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/FiltroBusqueda/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _filtroBusquedaService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/FiltroBusqueda
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] FiltroBusqueda filtroBusqueda)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            filtroBusqueda.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _filtroBusquedaService.Guardar(filtroBusqueda);
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

        // PUT api/FiltroBusqueda
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] FiltroBusqueda filtroBusqueda)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            filtroBusqueda.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _filtroBusquedaService.Modificar(filtroBusqueda);
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


        // DELETE api/FiltroBusqueda/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var filtroBusqueda = await _filtroBusquedaService.BuscarPorNumSec(codigo);
            filtroBusqueda.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _filtroBusquedaService.Eliminar(filtroBusqueda);
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

    }
}

