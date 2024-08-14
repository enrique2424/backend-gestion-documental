
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
using Application.Services.GestionDocumental;

namespace WebApi.Controllers.GestionDocumental
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserAreaController : ControllerBase
    {
        private readonly IUserAreaService _userareaService;

        public UserAreaController(IUserAreaService userareaService)
        {
            _userareaService = userareaService;
        }

        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _userareaService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // POST api/Seccion
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserArea seccion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
           //  seccion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _userareaService.Guardar(seccion);
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

        // GET api/UserArea/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _userareaService.BuscarPorNumSec(codigo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }


        // PUT api/UserArea
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] UserArea secretaria)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var respuestaBD = await _userareaService.Modificar(secretaria);
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

        // DELETE api/Secretaria/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var secretaria = await _userareaService.BuscarPorNumSec(codigo);
          //  secretaria.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _userareaService.Eliminar(secretaria);
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

