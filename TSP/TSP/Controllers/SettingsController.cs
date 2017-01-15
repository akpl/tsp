using System.Web.Http;
using TSP.Services;

namespace TSP.Controllers
{
    public class SettingsController : ApiController
    {
        private readonly IConfigurationService _config;
        public SettingsController(IConfigurationService config)
        {
            _config = config;
        }

        // GET api/Settings
        public Configuration Get()
        {
            return _config.Get();
        }

        // PUT api/Settings
        public void Put([FromBody]Configuration value)
        {
            _config.Save(value);
        }

        // DELETE: api/Settings
        public void Delete()
        {
            _config.Save(new Configuration());
        }
    }
}