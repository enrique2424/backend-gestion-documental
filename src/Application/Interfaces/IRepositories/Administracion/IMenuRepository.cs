using Application.DTOs.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories.Administracion
{
    public interface IMenuRepository
    {
        public Task<IEnumerable<MenuDto>> TraerMenuPorUsuario(long num_sec, string cuenta, string routerlink, long nsec_aplicacion);
    }
}
