using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TSP.Services;
using TSP.Solver;

namespace TSP.Controllers
{
    public class TargetsController : ApiController
    {
        public static IList<Target> Targets = new List<Target> { new Target { Name = "Rynek Główny", Location = new Coordinates(50.061820, 19.936709) } };
        // GET: api/Targets
        public IEnumerable<Target> Get()
        {
            return Targets;
        }

        // GET: api/Targets/5
        public Target Get(int id)
        {
            return Targets[id];
        }

        // POST: api/Targets
        public void Post([FromBody]Target value)
        {
            Targets.Add(value);
        }

        // PUT: api/Targets/5
        public void Put(int id, [FromBody]Target value)
        {
            Targets[id] = value;
        }

        // DELETE: api/Targets/5
        public void Delete(string id)
        {
            Targets.Remove(Targets.Single(target => target.Location.ToString() == id));
        }
    }
}
