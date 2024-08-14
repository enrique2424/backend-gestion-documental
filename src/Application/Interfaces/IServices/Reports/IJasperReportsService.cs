using Application.DTOs.Reports;
using Application.Interfaces.Common;
using Domain.Models.Data;
using Domain.Models.GestionDocumental;

namespace Application.Interfaces.IServices.Reports
{
    public interface IJasperReportsService
    {
        public Task<RespuestaCore> traerReportePDF(PostParametrosReporte objParametros, string extension = "pdf");

    }
}
