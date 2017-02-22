using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TSP.Services.OSRMDData;
using TSP.Solver;

namespace TSP.Services
{
    public class DistanceMatrixService : IDistanceService
    {
        private readonly string _serviceUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["OSRMApiUrl"];

        public string Unit => "m";

        public void FetchDistances(IList<Target> targets)
        {
            int size = targets.Count;
            Parallel.For(0, size, x =>
            {
                targets[x].Distances = new Dictionary<Target, double>(size);

                for (int y = 0; y < size; y++)
                {
                    if (x != y)
                    {
                        targets[x].Distances[targets[y]] = GetRoute(targets[x].Location, targets[y].Location).distance;
                    }
                }
            });
        }

        private OSRMDData.Route GetRoute(Coordinates origin, Coordinates destination)
        {
            Uri requestUri = CreateRequestUri(origin, destination);

            HttpClient client = new HttpClient();
            client.BaseAddress = requestUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(string.Empty).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(
                    $"Wystąpił błąd podczas odbierania danych. HTTP {(int) response.StatusCode}: {response.ReasonPhrase}");
            }

            Task<RouteResponse> dataObjects = response.Content.ReadAsAsync<RouteResponse>();
            RouteResponse responseContent = dataObjects.Result;

            if (responseContent == null)
            {
                throw new ApplicationException($"Błąd OSRM. Odebrano pusty obiekt.");
            }
            if (!responseContent.code.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new ApplicationException($"Błąd OSRM: {responseContent.code}");
            }
            if (responseContent.routes.Count != 1)
            {
                throw new ApplicationException($"Błąd OSRM. Otrzymano {responseContent.routes.Count} tras.");
            }

            return responseContent.routes.First();
        }

        private Uri CreateRequestUri(Coordinates origin, Coordinates destination)
        {
            string request = $"{_serviceUrl}/route/v1/driving/{origin.ToOSRMString()};{destination.ToOSRMString()}?overview=false";
            return new Uri(request);
        }
    }
}