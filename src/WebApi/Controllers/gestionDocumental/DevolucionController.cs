
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
    public class DevolucionController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;
        private readonly IDevolucionService _devolucionService;
        private readonly IDetPrestamoService _detPrestamoService;
        private readonly IDetDevolucionService _detDevolucionService;

        public DevolucionController(IDevolucionService devolucionService,
            IDetPrestamoService detPrestamoService,
            IDetDevolucionService detDevolucionService,
            IPrestamoService prestamoService)
        {
            _devolucionService = devolucionService;
            _detPrestamoService = detPrestamoService;
            _detDevolucionService = detDevolucionService;
            _prestamoService = prestamoService;
        }

        // GET: api/Devolucion
        [HttpGet]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = await _devolucionService.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar);
            return Ok(respuestaListado);
        }

        // GET api/Devolucion/5
        [HttpGet("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Get(long codigo)
        {
            var datos = await _devolucionService.BuscarPorNumSec(codigo);    
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);
        }

        // POST api/Devolucion
        [HttpPost]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Post([FromBody] DevolucionMasterDto devolucion)
        {
            var prestamosDevueltos = 0;
            //cambio el estado del det_prestamo para decirle q ya han sido devueltos
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            foreach (var d in devolucion.detallePrestamo) {
                var detalleModificar = new DetPrestamo()
                {
                    num_sec = d.num_sec,
                    nsec_prestamo = d.nsec_prestamo,
                    nsec_documento = d.nsec_documento,
                    estado_devolucion = d.estado_devolucion,
                    estado = "AC",
                    nsec_usuario_registro = long.Parse(nsecUsuario),
                };
                var res = await _detPrestamoService.Modificar(detalleModificar);
            }
            //busco si existe la devolucion, caso contrario realizo su creacion
            var respuestaDevolucionBD =  await _devolucionService.BuscarPorNumSec(devolucion.devolucion.num_sec);
            if (respuestaDevolucionBD == null)
            {
                //aqui deveria cambiar el estado del prestamo a PARCIAL

                //creo el objDevolucion y lo guardo una unica vez
                var objDevolucion = new Devolucion();
                objDevolucion.num_sec = 0;
                objDevolucion.nsec_prestamo = devolucion.prestamo.num_sec;
                objDevolucion.nsec_persona = devolucion.devolucion.nsec_persona;
                objDevolucion.fecha_devolucion = DateTime.Now;
                objDevolucion.observacion = devolucion.devolucion.observacion;
                objDevolucion.estado = "AC";
                var res = await _devolucionService.Guardar(objDevolucion);
                
                //una vez guardada la Devolucion, con ese Id es utilizado para
                //guardar los detalles
                if (res.status ==Status.Success) {
                    foreach (var detdevo in devolucion.detallePrestamo) {
                        //buscamos si existe este detalle y lo creamos
                        var respuestaDetDev = await _detDevolucionService.TraerDetDevoXPrestamoDocumento(res.numsec.ToString(), detdevo.nsec_documento.ToString());
                        if (respuestaDetDev== null && detdevo.estado_devolucion==1) {

                            var objDetDevolucion = new DetDevolucion();
                            objDetDevolucion.num_sec = 0;
                            objDetDevolucion.nsec_devolucion = long.Parse(res.numsec);
                            objDetDevolucion.nsec_documento = detdevo.nsec_documento;
                            objDetDevolucion.observacion = "";
                            objDetDevolucion.estado = "AC";
                            objDetDevolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
                            var resp = _detDevolucionService.Guardar(objDetDevolucion);
                        }
                            
                    }
                }
                return Ok(res);
                //var respuestaError = new RespuestaError()
                //{
                //    error = respuestaBD.status,
                //    message = respuestaBD.response
                //};
                //return BadRequest(respuestaError);
            }
            else {
                //en caso de que ya exista una devolucion
                //creo el objDevolucion para modificarlo

                //var objDevolucion = new Devolucion();
                //objDevolucion.num_sec = respuestaDevolucionBD.num_sec;
                //objDevolucion.nsec_prestamo = devolucion.prestamo.num_sec;
                //objDevolucion.nsec_persona = devolucion.devolucion.nsec_persona;
                //objDevolucion.fecha_devolucion = DateTime.Now;
                //objDevolucion.observacion = devolucion.devolucion.observacion;
                //objDevolucion.estado = "AC";
                //var res = await _devolucionService.Modificar(objDevolucion);

                //una vez Modificada la Devolucion, con ese Id es utilizado para
                //guardar los detalles
                //if (res.status == Status.Success)
                //{
                    foreach (var detdevo in devolucion.detallePrestamo)
                    {
                    var respuestaDetDev = await _detDevolucionService.TraerDetDevoXPrestamoDocumento(devolucion.devolucion.num_sec.ToString(),detdevo.nsec_documento.ToString());
                    if (respuestaDetDev==null && detdevo.estado_devolucion == 1) {
                        var objDetDevolucion = new DetDevolucion();
                        objDetDevolucion.num_sec = detdevo.num_sec;
                        objDetDevolucion.nsec_devolucion = respuestaDevolucionBD.num_sec;
                        objDetDevolucion.nsec_documento = detdevo.nsec_documento;
                        objDetDevolucion.observacion = "";
                        objDetDevolucion.estado = "AC";
                        objDetDevolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
                        var resp = _detDevolucionService.Guardar(objDetDevolucion);
                    }
                }
                    
                //}

            }
            //con el forescah voy a contar su todos los documentos estan en estado devuelto
            foreach (var estado in devolucion.detallePrestamo) {
                if (estado.estado_devolucion == 1) { 
                    prestamosDevueltos++;
                }
            }
            if (prestamosDevueltos == devolucion.detallePrestamo.Count)
            {

                var prestamo = new Prestamo();
                prestamo.num_sec = devolucion.prestamo.num_sec;
                prestamo.cod_prestamo = devolucion.prestamo.cod_prestamo;
                prestamo.nsec_persona = devolucion.prestamo.nsec_persona;
                prestamo.cargo_persona = devolucion.prestamo.cargo_persona;
                prestamo.fecha_prestamo = devolucion.prestamo.fecha_prestamo;
                prestamo.fecha_devolucion = devolucion.prestamo.fecha_devolucion;
                prestamo.nsec_secretaria = devolucion.prestamo.nsec_secretaria;
                prestamo.observacion = devolucion.prestamo.observacion;
                prestamo.nsec_usuario = devolucion.prestamo.nsec_usuario;
                prestamo.nsec_estado_prestamo = 3;
                prestamo.estado = "AC";
                prestamo.nsec_area = devolucion.prestamo.nsec_area;
                prestamo.telefono = devolucion.prestamo.telefono;
                prestamo.nro_interno = devolucion.prestamo.nro_interno;
                prestamo.obs_externo = devolucion.prestamo.obs_externo;
                prestamo.externo = devolucion.prestamo.externo;
                await _prestamoService.Modificar(prestamo);
            }
            else {
                var prestamo = new Prestamo();
                prestamo.num_sec = devolucion.prestamo.num_sec;
                prestamo.cod_prestamo = devolucion.prestamo.cod_prestamo;
                prestamo.nsec_persona = devolucion.prestamo.nsec_persona;
                prestamo.cargo_persona = devolucion.prestamo.cargo_persona;
                prestamo.fecha_prestamo = devolucion.prestamo.fecha_prestamo;
                prestamo.fecha_devolucion = devolucion.prestamo.fecha_devolucion;
                prestamo.nsec_secretaria = devolucion.prestamo.nsec_secretaria;
                prestamo.observacion = devolucion.prestamo.observacion;
                prestamo.nsec_usuario = devolucion.prestamo.nsec_usuario;
                prestamo.nsec_estado_prestamo = 2;
                prestamo.estado = "AC";
                prestamo.nsec_area = devolucion.prestamo.nsec_area;
                prestamo.telefono = devolucion.prestamo.telefono;
                prestamo.nro_interno = devolucion.prestamo.nro_interno;
                prestamo.obs_externo = devolucion.prestamo.obs_externo;
                prestamo.externo = devolucion.prestamo.externo;
                await _prestamoService.Modificar(prestamo);
            }
            return Ok(respuestaDevolucionBD);
        }

        // PUT api/Devolucion
        [HttpPut]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Put([FromBody] Devolucion devolucion)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            devolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _devolucionService.Modificar(devolucion);
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


        // DELETE api/Devolucion/5
        [HttpDelete("{codigo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> Delete(long codigo)
        {
            string nsecUsuario = User!.FindFirst(ClaimTypes.Sid)!.Value;
            var devolucion = await _devolucionService.BuscarPorNumSec(codigo);
            devolucion.nsec_usuario_registro = long.Parse(nsecUsuario);
            var respuestaBD = await _devolucionService.Eliminar(devolucion);
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
        [HttpGet("TraerDevolucionoMasterXcodPrestamo/{cod_prestamo}")]
        [Authorize(Roles = Roles.Todos)]
        public async Task<ActionResult> TraerPrestamoMasterXcodPrestamo(string cod_prestamo)
        {
            long nsecUsuario = long.Parse(User!.FindFirst(ClaimTypes.Sid)!.Value);
            int nsecRol = int.Parse(User!.FindFirst(ClaimCustom.NsecRol)!.Value);

            var respuestaListado = await _devolucionService.TraerDevXNsecPrestamo(cod_prestamo);
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = respuestaListado
            };
            return Ok(respuesta);
        }

    }
}

