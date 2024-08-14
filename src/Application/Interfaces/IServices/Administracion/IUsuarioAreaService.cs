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
    public interface IUsuarioAreaService : IGenericService<UsuarioArea>
    {
        public Task<RespuestaListado<UsuarioAreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaListado<UsuarioAreaDto>> BuscarListadoUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<RespuestaListado<UsuarioAreaDto>> BuscarListadoTodosUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
