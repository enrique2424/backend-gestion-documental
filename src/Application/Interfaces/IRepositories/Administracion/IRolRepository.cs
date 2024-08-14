using Application.DTOs.Administracion;
using Application.Interfaces.Common;
using Domain.Models.Administracion;

namespace Application.Interfaces.IRepositories.Administracion
{
    public interface IRolRepository: IGenericRepositoryAdministracion<Rol>
    {
        public Task<IEnumerable<RolDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar);
    }
}
