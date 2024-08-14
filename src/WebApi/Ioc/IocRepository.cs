using Application.Interfaces.Common;
using Application.Interfaces.IRepositories.Administracion;
using Application.Interfaces.IRepositories.GestionDocumental;
using Infrastructure.Repositories.Administracion;
using Infrastructure.Repositories.GestionDocumental;
using Infrastructure.Repositories.Common;
using Infrastructure.Repositories.Reports;
using Application.Interfaces.IRepositories.Reports;

namespace WebApi.Ioc
{
    public static class IocRepository
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IGenericRepositoryAdministracion<>), typeof(GenericRepositoryAdministracion<>));
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            // agregar todos los interfaces de repositorio y repositorrios
            services.AddTransient<IAdjuntoRepository, AdjuntoRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IDetAdjuntoTablaRepository, DetAdjuntoTablaRepository>();
            services.AddTransient<IDetDevolucionRepository, DetDevolucionRepository>();
            services.AddTransient<IDetPrestamoRepository, DetPrestamoRepository>();
            services.AddTransient<IDetSeccionFiltroBusquedaRepository, DetSeccionFiltroBusquedaRepository>();
            services.AddTransient<IDevolucionRepository, DevolucionRepository>();
            services.AddTransient<IDocumentoRepository, DocumentoRepository>();
            services.AddTransient<IEstadoDocumentoRepository, EstadoDocumentoRepository>();
            services.AddTransient<IEstadoPrestamoRepository, EstadoPrestamoRepository>();
            services.AddTransient<IEstanteRepository, EstanteRepository>();
            services.AddTransient<IFilaRepository, FilaRepository>();
            services.AddTransient<IFiltroBusquedaRepository, FiltroBusquedaRepository>();
            services.AddTransient<IGestionRepository, GestionRepository>();
            services.AddTransient<IIdentificadorRepository, IdentificadorRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IPersonaRepository, PersonaRepository>();
            services.AddTransient<ISgPersonaRepository, SgPersonaRepository>();
            services.AddTransient<ISgDocumentoIdentificacionRepository, SgDocumentoIdentificacionRepository>();
            services.AddTransient<ISgEstadoCivilRepository, SgEstadoCivilRepository>();
            services.AddTransient<IPrestamoRepository, PrestamoRepository>();
            services.AddTransient<ISeccionRepository, SeccionRepository>();
            services.AddTransient<ISecretariaRepository, SecretariaRepository>();
            services.AddTransient<ITablaRepository, TablaRepository>();
            services.AddTransient<ITipoAccesoRepository, TipoAccesoRepository>();
            services.AddTransient<ITipoDocumentoRepository, TipoDocumentoRepository>();
            services.AddTransient<ITipoPersonaRepository, TipoPersonaRepository>();
            services.AddTransient<ITomoRepository, TomoRepository>();
            services.AddTransient<IVolumenRepository, VolumenRepository>();
            services.AddTransient<IUsuarioAreaRepository, UsuarioAreaRepository>();
            services.AddTransient<IUserAreaRepository, UserAreaRepository>();
            services.AddTransient<IRolRepository, RolRepository>();
            services.AddTransient<IUserRolRepository, UserRolRepository>();
            services.AddTransient<IDocumentacionDerivadaRepository, DocumentacionDerivadaRepository>();
            services.AddTransient<IJasperReportsRepository, JasperReportsRepository>();


            return services;
        }
    }
}
