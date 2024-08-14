using Application.DTOs.Administracion;
using Application.Interfaces.Common;
using Domain.Models.Administracion;

namespace Application.Interfaces.IRepositories.Administracion
{
    public interface IUsuarioAreaRepository: IGenericRepositoryAdministracion<UsuarioArea>
    {
        public Task<IEnumerable<UsuarioAreaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<UsuarioAreaDto>> BuscarListadoUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
        public Task<IEnumerable<UsuarioAreaDto>> BuscarListadoTodosUsuario(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
