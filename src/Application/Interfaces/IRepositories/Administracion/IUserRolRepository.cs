using Application.DTOs.Administracion;
using Application.Interfaces.Common;
using Domain.Models.Administracion;

namespace Application.Interfaces.IRepositories.Administracion
{
    public interface IUserRolRepository: IGenericRepositoryAdministracion<UserRol>
    {
        public Task<IEnumerable<UserRolDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
