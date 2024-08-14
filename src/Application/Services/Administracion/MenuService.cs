using Application.DTOs.Administracion;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.IServices.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Administracion
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly long NSEC_APLICACION = Utils.Aplicacion.NSEC_APLICACION;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<IEnumerable<MenuDto>> TraerMenuPorUsuario(long num_sec, string cuenta, string routerlink)
        {
            var menus = await _menuRepository.TraerMenuPorUsuario(num_sec, cuenta, routerlink, NSEC_APLICACION);
            return menus;
        }
    }
}
