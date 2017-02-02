using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP.Services
{
    public class DistanceServiceFactory : IDistanceServiceFactory
    {
        private readonly IConfigurationService _configurationService;

        public DistanceServiceFactory(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public IDistanceService Build()
        {
            switch (_configurationService.Get().Engine)
            {
                case DistanceResolverType.GoogleMapsDistance:
                    return new GoogleMapsDistanceMatrixService();
                case DistanceResolverType.OSRMDistance:
                    return new DistanceMatrixService();
                    case DistanceResolverType.OSRMTime:
                    return new OSRMTableService();
                default:
                    return null;
            }
        }
    }
}