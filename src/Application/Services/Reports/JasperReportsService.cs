using Application.DTOs.Reports;
using Application.Interfaces.IRepositories.Reports;
using Application.Interfaces.IServices.Reports;
using Application.Services.Common;
using Application.Utils;
using Domain.Models.Data;
using System.Transactions;

namespace Application.Services.Reports
{
    public class JasperReportsService : IJasperReportsService
    {
        private readonly IJasperReportsRepository _jasperReportsRepository;

        public JasperReportsService(IJasperReportsRepository jasperReportsRepository)
        {
            _jasperReportsRepository = jasperReportsRepository;
        }

        public async Task<RespuestaCore> traerReportePDF(PostParametrosReporte objParametros, string extension = "pdf")
        {
            return await _jasperReportsRepository.traerReportePDF(objParametros, extension);
        }

    }
}
