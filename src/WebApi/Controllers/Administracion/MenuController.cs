using Application.Interfaces.IServices.Administracion;
using Application.Utils;
using Domain.Models.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace WebApi.Controllers.Administracion
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [Route("traer_menu_por_usuario")]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> TraerMenuPorUsuario()
        {

            var cuenta = User!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var datos = await _menuService.TraerMenuPorUsuario(0, cuenta, "");
            var respuesta = new RespuestaCore()
            {
                status = Status.Success,
                response = datos
            };
            return Ok(respuesta);

        }

    }
}
