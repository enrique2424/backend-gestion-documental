using Application.DTOs.Administracion;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices.Administracion
{
    public interface IRolService : IGenericService<Rol>
    {
        public Task<RespuestaListado<RolDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
