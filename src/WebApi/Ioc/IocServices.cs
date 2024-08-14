using Application.Interfaces.IServices.Administracion;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Administracion;
using Application.Services.GestionDocumental;
using Application.Services.Administracion.Login;
using Application.Services.gestionDocumental;
using Application.Utils;
using Application.Interfaces.Common;
using Application.Services.Common;
using Application.Interfaces.IData;
using Infrastructure.Persistence;
using Application.Interfaces.IServices.Reports;
using Application.Services.Reports;

namespace WebApi.Ioc
{
    public static class IocServices
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<LoginFactory>();

            services.AddScoped<LoginService>()
                .AddScoped<ILoginService, LoginService>(s => s.GetService<LoginService>()!);

            services.AddScoped<LoginActiveDirectoryService>()
                .AddScoped<ILoginService, LoginActiveDirectoryService>(s => s.GetService<LoginActiveDirectoryService>()!);


            services.AddTransient<IMenuService, MenuService>();

            // agregar para usar el controlador           
            services.AddTransient<IAdjuntoService, AdjuntoService>();
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IDetAdjuntoTablaService, DetAdjuntoTablaService>();
            services.AddTransient<IDetDevolucionService, DetDevolucionService>();
            services.AddTransient<IDetPrestamoService, DetPrestamoService>();
            services.AddTransient<IDetSeccionFiltroBusquedaService, DetSeccionFiltroBusquedaService>();
            services.AddTransient<IDevolucionService, DevolucionService>();
            services.AddTransient<IDocumentoService, DocumentoService>();
            services.AddTransient<IEstadoDocumentoService, EstadoDocumentoService>();
            services.AddTransient<IEstadoPrestamoService, EstadoPrestamoService>();
            services.AddTransient<IEstanteService, EstanteService>();
            services.AddTransient<IFilaService, FilaService>();
            services.AddTransient<IFiltroBusquedaService, FiltroBusquedaService>();
            services.AddTransient<IGestionService, GestionService>();
            services.AddTransient<IIdentificadorService, IdentificadorService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IPersonaService, PersonaService>();
            services.AddTransient<ISgPersonaService, SgPersonaService>();
            services.AddTransient<ISgEstadoCivilService, SgEstadoCivilService>();
            services.AddTransient<ISgDocumentoIdentificacionService, SgDocumentoIdentificacionService>();
            services.AddTransient<IPrestamoService, PrestamoService>();
            services.AddTransient<ISeccionService, SeccionService>();
            services.AddTransient<ISecretariaService, SecretariaService>();
            services.AddTransient<ITablaService, TablaService>();
            services.AddTransient<ITipoAccesoService, TipoAccesoService>();
            services.AddTransient<ITipoDocumentoService, TipoDocumentoService>();
            services.AddTransient<ITipoPersonaService, TipoPersonaService>();
            services.AddTransient<ITomoService, TomoService>();
            services.AddTransient<IVolumenService, VolumenService>();
            services.AddTransient<IUsuarioAreaService, UsuarioAreaService>();
            services.AddTransient<IApplicationDbContextAdministracion, ApplicationDbContextAdministracion>();
            services.AddTransient<IUserAreaService, UserAreaService>();
            services.AddTransient<IRolService, RolService>();
            services.AddTransient<IUserRolService, UserRolService>();
            services.AddTransient<IDocumentacionDerivadaService, DocumentacionDerivadaService>();




            services.AddTransient<IJasperReportsService, JasperReportsService>();
            services.AddSingleton<ArchivoMongoService>();
            services.AddSingleton<IFichero, Fichero>();
            return services;
        }
    }
}
