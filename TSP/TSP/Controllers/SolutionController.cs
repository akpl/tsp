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
    public class SolutionController : ApiController
    {
        private readonly ITargetsService _targetsService;
        private readonly IDistanceService _distanceService;

        public SolutionController(ITargetsService targetsService, IDistanceServiceFactory distanceServiceFactory)
        {
            _targetsService = targetsService;
            _distanceService = distanceServiceFactory.Build();
        }

        // GET: api/Solution
        public string Get()
        {
            return "Invalid method";
        }

        // POST: api/Solution
        public RoutingResult Post()
        {
            var targets = _targetsService.Read();
            
            _distanceService.FetchDistances(targets);

            ISolver solver = new GeneticSolver();
            Route bestRoute = solver.Solve(new TargetsCollection(targets));

            return new RoutingResult(bestRoute);
        }
    }
}
