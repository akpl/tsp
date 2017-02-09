﻿using System.Collections.Generic;
using System.Web.Http;
using TSP.Services;
using TSP.Solver;

namespace TSP.Controllers
{
    public class SolutionController : ApiController
    {
        private readonly ITargetsService _targetsService;
        private readonly IDistanceService _distanceService;
        private readonly Configuration _configuration;

        public SolutionController(
            ITargetsService targetsService, 
            IDistanceServiceFactory distanceServiceFactory, 
            IConfigurationService configurationService)
        {
            _targetsService = targetsService;
            _distanceService = distanceServiceFactory.Build();
            _configuration = configurationService.Get();
        }
        
        // POST: api/Solution
        public RoutingResult Post()
        {
            List<Target> targets = _targetsService.Read();
            
            _distanceService.FetchDistances(targets);

            ISolver solver = new GeneticSolver
            {
                GenerationsCount = _configuration.GenerationsCount,
                MutationRate = _configuration.MutationRate,
                PopulationSize = _configuration.PopulationSize,
                TournamentSize = _configuration.TournamentSize
            };

            Route bestRoute = solver.Solve(new TargetsCollection(targets));

            return new RoutingResult(bestRoute);
        }
    }
}
