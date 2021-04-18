using EBusValidator.Core;
using EBusValidator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace EBusValidator.API.Controllers
{
    public class SmartcardController : BaseApiController
    {
        SmartcardService service = new SmartcardService();

        [Route("api/Smartcard/GetSmartcards")]
        //public async Task<IHttpActionResult> GetSmartcards()
        //{
        //    try
        //    {
        //        return await CreateHttpResponse(() =>
        //        {
        //            List<SmartcardModel> response = null;
        //            new Task(() => {
        //                response = service.GetSmartCards();
        //            }).Start();

        //            return Ok(response);
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return Ok(false);
        //    }
        //}
        public async Task<List<SmartcardModel>> GetSmartcards()
        {
            try
            {
                return await Task.Run(() => service.GetSmartCards());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        [Route("api/Smartcard/GetSmartcard")]
        public async Task<SmartcardModel> GetSmartcard(int smartCardID)
        {
            try
            {
                return await Task.Run(() => service.GetSmartCard(smartCardID));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        [Route("api/Smartcard/InsertOrUpdateSmartcard")]
        [HttpPost]
        public async Task<string> InsertOrUpdateSmartcard(SmartcardModel smartcard)
        {
            try
            {
                return await Task.Run(() => service.InsertOrUpdateSmartcard(smartcard));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}