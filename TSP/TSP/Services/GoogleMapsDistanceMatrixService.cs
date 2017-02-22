using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Configuration;
using TSP.Services.GoogleMaps;
using TSP.Solver;

namespace TSP.Services
{
    public class GoogleMapsDistanceMatrixService : IDistanceService
    {
        private readonly string _apiKey = WebConfigurationManager.AppSettings["GoogleMapsApiKey"];

        public string Unit => "m";

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

            Task<DistanceMatrix> dataObjects = response.Content.ReadAsAsync<DistanceMatrix>();
            DistanceMatrix responseContent = dataObjects.Result;

            if (responseContent == null)
            {
                throw new ApplicationException($"Błąd API Google Maps. Odebrano pusty obiekt.");
            }
            if (!responseContent.status.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                throw new ApplicationException($"Błąd API Google Maps: {responseContent.status}");
            }

            int totalRows = targets.Count;

            if (responseContent.rows.Count != totalRows)
            {
                throw new ApplicationException($"Błąd API Google Maps. Otrzymano {responseContent.rows.Count} tras.");
            }

            List<Row> rows = responseContent.rows;

            for (int fromIndex = 0; fromIndex < totalRows; ++fromIndex)
            {
                targets[fromIndex].Distances = new Dictionary<Target, double>();

                for (int toIndex = 0; toIndex < totalRows; ++toIndex)
                {
                    if (fromIndex != toIndex)
                    {
                        targets[fromIndex].Distances.Add(targets[toIndex],
                            rows[fromIndex].elements[toIndex].distance.value);
                    }
                }
            }
        }

        private Uri CreateRequestUri(IList<Target> targets)
        {

            string coords = string.Join("|", targets.Select(target => target.Location.ToDecimalDegreesString()));
            string request = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={coords}&destinations={coords}&key={_apiKey}";

            return new Uri(request);
        }
    }
}