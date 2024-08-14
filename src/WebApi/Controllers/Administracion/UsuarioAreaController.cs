using Application.Interfaces.IServices.Administracion;
using Application.Utils;
using Domain.Models.Administracion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers.Administracion
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuario_AreaController : ControllerBase
    {

        private readonly IUsuarioAreaService _areaService;

        public Usuario_AreaController(IUsuarioAreaService areaService)
        {
            _areaService = areaService;
        }


        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _areaService.BuscarListado (valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET: api/Area
        [HttpGet("TraerUsuarioNV")]
        public async Task<ActionResult> GetTraerUsuarioNV(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
          
            var respuestaListado = await _areaService.BuscarListadoUsuario(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET: api/Area
        [HttpGet("TraerTodosUsuarioNV")]
        public async Task<ActionResult> GetTraerTodosUsuarioNV(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {

            var respuestaListado = await _areaService.BuscarListadoTodosUsuario(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET: api/<Usuario_Area>


        // GET api/<Usuario_Area>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Usuario_Area>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Usuario_Area>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Usuario_Area>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
