using System.Net.Http;
using System.Web.Http;
using GetEmpStatus.MyClasses;

namespace GetEmpStatus.Controllers
{
    public class UsersController : ApiController
    {
        private ProcessStatus processStatus;

        public UsersController()
        {
            string connectionString = "server=.\\HMSSQLSERVER; database=PSSEmployeesSalaries; Integrated Security=true;";
            processStatus = new ProcessStatus(connectionString);
        }

        [HttpGet]
        [Route("api/users")]
        public HttpResponseMessage GetUsersInfo()
        {
            return processStatus.GetAllUsersInfo();
        }

        [HttpGet]
        [Route("api/users/{userNatNum}")]
        public HttpResponseMessage GetUserByNatId(int userNatNum)
        {
            return processStatus.GetUserByNatId(userNatNum);
        }
    }
}
