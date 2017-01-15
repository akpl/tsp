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
        private readonly ITargetsService _targetsService;

        public TargetsController(ITargetsService targetsService)
        {
            _targetsService = targetsService;
        }

        // GET: api/TargetsService
        public IEnumerable<Target> Get()
        {
            return _targetsService.Read();
        }

        // GET: api/TargetsService/5
        public Target Get(int id)
        {
            return _targetsService.Read()[id];
        }

        // POST: api/TargetsService
        public void Post([FromBody]Target value)
        {
            var targets = _targetsService.Read();
            targets.Add(value);
            _targetsService.Save(targets);
        }

        // DELETE: api/TargetsService/5
        public void Delete(string id)
        {
            var targets = _targetsService.Read();
            Guid idToDelete = Guid.Parse(id);
            Target targetToDelete = targets.Single(target => target.Id == idToDelete);
            if (targetToDelete == null)
            {
                return;
            }

            targets.Remove(targetToDelete);
            _targetsService.Save(targets);
        }
    }
}
