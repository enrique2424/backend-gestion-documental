
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
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        private readonly IDetPrestamoService _detPrestamoService;

        public PrestamoController(IPrestamoService prestamoService,IDetPrestamoService detPrestamoService)
        {
            _prestamoService = prestamoService;
            _detPrestamoService = detPrestamoService;
        }
        //con este metodo carga el buscar
        // GET: api/Prestamo
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        
        {
            var respuestaListado = await _prestamoService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Prestamo/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _prestamoService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Prestamo
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] PrestamoMasterDto prestamo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detalleDocumento = prestamo.detallePrestamo;
            prestamo.prestamo.fecha_prestamo = DateTime.Now;
            prestamo.prestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            prestamo.prestamo.nsec_estado_prestamo=1;
            prestamo.prestamo.estado = "AC"; 
            var respuestaBD = await _prestamoService.Guardar(prestamo.prestamo);

            foreach (var f in prestamo.detallePrestamo)
            {
                var detalle = new DetPrestamo()
                {
                    num_sec=0,
                    nsec_prestamo = long.Parse(respuestaBD.numsec),
                    nsec_documento=f.nsec_documento,
                    estado_devolucion=0,
                    estado = "AC",
                    nsec_usuario_registro = long.Parse(nsecUsuario),
                };
                var res = await _detPrestamoService.Guardar(detalle);
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

        // PUT api/Prestamo
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] PrestamoMasterDto prestamo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var detalleDocumento = prestamo.detallePrestamo;

            prestamo.prestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            prestamo.prestamo.nsec_estado_prestamo = 1;
            prestamo.prestamo.estado = "AC";

            var respuestaBD = await _prestamoService.Modificar(prestamo.prestamo);

            var listaDetalles = await _prestamoService.TraerDetallePrestamoPorPrestamo(Convert.ToInt32(prestamo.prestamo.num_sec));

            foreach (var item in listaDetalles)
            {
                DetPrestamo tipoDet = new DetPrestamo();
                tipoDet.num_sec = Convert.ToInt32(item.num_sec);
                tipoDet.nsec_prestamo = item.nsec_prestamo;
                tipoDet.nsec_documento = item.nsec_documento;
                tipoDet.estado_devolucion = item.estado_devolucion;
                tipoDet.estado = item.estado;

                var res = await _detPrestamoService.Eliminar(tipoDet);
            }

            foreach (var f in prestamo.detallePrestamo)
            {

                if (f.num_sec == 0)
                {
                    var detalleNuevo = new DetPrestamo()
                    {
                        num_sec = 0,
                        nsec_prestamo = long.Parse(respuestaBD.numsec),
                        nsec_documento = f.nsec_documento,
                        estado_devolucion = f.estado_devolucion,
                        estado = "AC",
                        nsec_usuario_registro = long.Parse(nsecUsuario),
                    };
                    var filtroPorGuardar = await _detPrestamoService.Guardar(detalleNuevo);
                }
                else {
                    var detalleModificar = new DetPrestamo()
                    {
                        num_sec = f.num_sec,
                        nsec_prestamo = f.nsec_prestamo,
                        nsec_documento = f.nsec_documento,
                        estado_devolucion = f.estado_devolucion,
                        estado = "AC",
                        nsec_usuario_registro = long.Parse(nsecUsuario),
                    };
                    var res = await _detPrestamoService.Modificar(detalleModificar);
                }
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


        //DELETE api/Prestamo/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var prestamo = await _prestamoService.BuscarPorNumSec(codigo);

            PrestamoMasterDto detalles=new PrestamoMasterDto();
             detalles = _prestamoService.TraerPrestamoMaster(Convert.ToInt32(prestamo.num_sec)).Result;

            prestamo.nsec_usuario_registro = long.Parse(nsecUsuario);
            
            var respuestaBD = await _prestamoService.Eliminar(prestamo);
            foreach (var f in detalles.detallePrestamo)
            {
                var detalle = new DetPrestamo()
                {
                    num_sec = f.num_sec,
                    nsec_prestamo = long.Parse(respuestaBD.numsec),
                    nsec_documento = f.nsec_documento,
                    estado_devolucion = 0,
                    estado = "BA",
                    nsec_usuario_registro = long.Parse(nsecUsuario),
                };
                
                var res = await _detPrestamoService.Eliminar(detalle);
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

        //llamamos el prestamos para ser editado, atraves del num_sec que nos pasan
        [HttpGet("TraerPrestamoMaster/{nsec_prestamo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> TraerPrestamoMaster(int nsec_prestamo)
        {
            long nsecUsuario = long.Parse(User!.FindFirst(ClaimTypes.Sid)!.Value);
            int nsecRol = int.Parse(User!.FindFirst(ClaimCustom.NsecRol)!.Value);

            var respuestaListado = await _prestamoService.TraerPrestamoMaster(nsec_prestamo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = respuestaListado
            };
            return Ok(respuesta);
        }

        //llamamos el prestamos para ser editado, atraves del num_sec que nos pasan
        [HttpGet("TraerPrestamoMasterXcodPrestamo/{cod_prestamo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> TraerPrestamoMasterXcodPrestamo(string cod_prestamo)
        {
            long nsecUsuario = long.Parse(User!.FindFirst(ClaimTypes.Sid)!.Value);
            int nsecRol = int.Parse(User!.FindFirst(ClaimCustom.NsecRol)!.Value);

            var respuestaListado = await _prestamoService.TraerPrestamoMasterXcodPrestamo(cod_prestamo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = respuestaListado
            };
            return Ok(respuesta);
        }
        

    }
}

