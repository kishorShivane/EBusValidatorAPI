using EBusValidator.Core;
using EBusValidator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace EBusValidator.API.Controllers
{
    public class ReportsController : BaseApiController
    {
        ReportsService service = new ReportsService();

        [Route("api/Reports/GetAllUsageSummary")]
        public async Task<List<UsageSummaryModel>> GetAllUsageSummary()
        {
            try
            {
                return await Task.Run(() => service.GetAllUsageSummary());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost]
        [Route("api/Reports/GetUsageSummaries")]
        public async Task<List<UsageSummaryModel>> GetUsageSummaries([FromBody] SearchParams searchParams)
        {
            try
            {
                return await Task.Run(() => service.GetUsageSummaries(searchParams));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost]
        [Route("api/Reports/GetUsageHistory")]
        public async Task<List<UsageHistoryModel>> GetUsageHistory([FromBody] SearchParams searchParams)
        {
            try
            {
                return await Task.Run(() => service.GetUsageHistory(searchParams.FromDate, searchParams.ToDate, searchParams.Smartcard));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
