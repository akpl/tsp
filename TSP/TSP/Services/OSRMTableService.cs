using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TSP.Services.OSRMDData;
using TSP.Solver;

namespace TSP.Services
{
    public class OSRMTableService : IDistanceService
    {
        private readonly string _serviceUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["OSRMApiUrl"];

        public void FetchDistances(IList<Target> targets)
        {
            Uri requestUri = CreateRequestUri(targets);

            HttpClient client = new HttpClient();
            client.BaseAddress = requestUri;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(string.Empty).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(
                    $"Wystąpił błąd podczas odbierania danych. HTTP {(int)response.StatusCode}: {response.ReasonPhrase}");
            }

            Task<TableResponse> dataObjects = response.Content.ReadAsAsync<TableResponse>();
            TableResponse responseContent = dataObjects.Result;

            if (responseContent == null)
            {
                throw new ApplicationException($"Błąd API OSRM. Odebrano pusty obiekt.");
            }
            if (!responseContent.code.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new ApplicationException($"Błąd API OSRM: {responseContent.code}");
            }

            int totalRows = targets.Count;

            if (responseContent.durations.Count != totalRows)
            {
                throw new ApplicationException($"Błąd API OSRM. Otrzymano {responseContent.durations.Count} tras.");
            }

            List<List<double>> rows = responseContent.durations;

            for (int fromIndex = 0; fromIndex < totalRows; ++fromIndex)
            {
                targets[fromIndex].Distances = new Dictionary<Target, double>();

                for (int toIndex = 0; toIndex < totalRows; ++toIndex)
                {
                    if (fromIndex != toIndex)
                    {
                        targets[fromIndex].Distances.Add(targets[toIndex], rows[fromIndex][toIndex]);
                    }
                }
            }
        }
        
        private Uri CreateRequestUri(IEnumerable<Target> targets)
        {
            string coords = string.Join(";", targets.Select(target => target.Location.ToOSRMString()));
            string request = $"{_serviceUrl}/table/v1/driving/{coords}";

            return new Uri(request);
        }
    }
}