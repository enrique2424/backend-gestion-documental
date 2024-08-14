using Application.Interfaces.IServices.Administracion;
using Application.Utils;
using Domain.Models.Administracion;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Administracion
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolController : ControllerBase
    {

        private readonly IUserRolService _userrolService;

        public UserRolController(IUserRolService userrolService)
        {
            _userrolService = userrolService;
        }


        [HttpGet]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _userrolService.BuscarListado (valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }


        // POST api/Seccion
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserRol seccion)
        {
         
            //  seccion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _userrolService.Guardar(seccion);
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
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _userrolService.BuscarPorNumSec(codigo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }


        // PUT api/UserArea
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserRol secretaria)
        {
          //  string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var respuestaBD = await _userrolService.Modificar(secretaria);
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
        public async Task<ActionResult> Delete(long codigo)
        {
          //  string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var secretaria = await _userrolService.BuscarPorNumSec(codigo);
            //  secretaria.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _userrolService.Eliminar(secretaria);
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
