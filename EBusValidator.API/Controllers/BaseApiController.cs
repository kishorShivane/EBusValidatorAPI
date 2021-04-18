using EBusValidator.Logger;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace EBusValidator.API.Controllers
{
    public class BaseApiController : ApiController
    {
        public LoggerManager logger { get; set; }

        public BaseApiController()
        {
            logger = new LoggerManager();
        }

        /// <summary>
        /// Create Http Response after method invoke
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> CreateHttpResponse(Func<IHttpActionResult> function)
        {

            IHttpActionResult response = null;
            try
            {

                await Task.Run(() =>
                {
                    response = function.Invoke();
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}