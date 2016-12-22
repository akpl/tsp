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
        // GET: api/Solution
        public string Get()
        {
            return "Invalid method";
        }

        // POST: api/Solution
        public Route Post()
        {
            DistanceMatrixService service = new DistanceMatrixService();
            service.FetchDistances(TargetsController.Targets);

            ISolver solver = new GeneticSolver();
            return solver.Solve(new TargetsCollection(TargetsController.Targets));
        }
    }
}
