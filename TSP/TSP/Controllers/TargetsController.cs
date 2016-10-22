using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSP.Solver;

namespace TSP.Controllers
{
    public class TargetsController : ApiController
    {
        private static IList<Target> _targets = new List<Target> { new Target { Name = "Rynek Główny 1" } };
        // GET: api/Targets
        public IEnumerable<Target> Get()
        {
            return _targets;
        }

        // GET: api/Targets/5
        public Target Get(int id)
        {
            return _targets[id];
        }

        // POST: api/Targets
        public void Post([FromBody]Target value)
        {
            _targets.Add(value);
        }

        // PUT: api/Targets/5
        public void Put(int id, [FromBody]Target value)
        {
            _targets[id] = value;
        }

        // DELETE: api/Targets/5
        public void Delete(int id)
        {
            _targets.RemoveAt(id);
        }
    }
}
