using EBusValidator.Models;
using System.Web.Http;

namespace EBusValidator.API.Controllers
{
    public class UserController : BaseApiController
    {
        [HttpGet]
        public string Index()
        {
            return "Method called";
        }

        [Route("api/User/ValidateUser")]
        [HttpPost]
        public UserModel ValidateUser([FromBody] UserModel user)
        {
            if (user.UserName.ToLower() == "admin" && user.Password.ToLower() == "admin")
            {
                return new UserModel() { FirstName = "Pavan", LastName = "Kumar", Role = "admin", UserName = "admin" };
            }
            return null;
        }
    }
}
