using BLUE.API.Abstractions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Http;

namespace BLUE.API.Controllers
{
    [RoutePrefix("api/servers")]
    public class DayZServerController : ApiController
    {
        private readonly IDayZServerRepository _dayZServerRepository;

        public DayZServerController(IDayZServerRepository dayZServerRepository)
        {
            _dayZServerRepository = dayZServerRepository;
        }

        [HttpGet]
        [Route("list")]
        public async Task<string> GetServerListAsync()
        {
            var servers = await _dayZServerRepository.GetAllServersAsync();

            string respose = JsonConvert.SerializeObject(servers);

            return respose;
        }
    }
}