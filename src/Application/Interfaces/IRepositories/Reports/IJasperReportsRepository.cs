using Application.DTOs.Reports;
using Domain.Models.Data;

namespace Application.Interfaces.IRepositories.Reports
{
    public interface IJasperReportsRepository
    {
        public Task<RespuestaCore> traerReportePDF(PostParametrosReporte objParametros, string extension = "pdf");

    }
}
