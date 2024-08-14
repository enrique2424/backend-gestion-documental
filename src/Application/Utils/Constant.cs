using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class Aplicacion
    {
        public static readonly long NSEC_APLICACION = 6;
    }
    public static class Estados
    {
        public const string Activo = "AC";
        public const string Aprobado = "AP";
        public const string Baja = "BA";
        public const string Pendiente = "PE";
        public const string Consolidado = "CO";
        public const string Rechazado = "RE";

    }

    public static class Status
    {
        public const string Empty = "empty";
        public const string Success = "success";
        public const string Error = "error";
        public const string Error500 = "500";

    }

    public static class Roles
    {
        public const string Todos = "Administrador, Digitador";
        public const string Administrador = "Administrador";
        public const string AdministradorSupervisor = "Administrador, Supervisor";
        public const string AdministradorAnalista = "Administrador, Analista";
        public const string AdministradorAdministrativo = "Administrador, Administrativo";
        public const string AdministradorSupervisorAnalista = "Administrador, Supervisor, Analista";

    }
    public static class ClaimCustom
    {
        public const string NsecUsuario = "sid";
        public const string NsecRol = "nsecRol";
    }

    public static class JasperReportObject
    {
        public const int VALOR_BLANCO_PDF = 987;
    }
}
