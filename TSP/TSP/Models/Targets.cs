using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TSP.Solver;

namespace TSP.Models
{
    public class Targets
    {
        private static readonly string FilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "tsp.json");

        public static List<Target> Read()
        {
            try
            {
                using (StreamReader stream = new StreamReader(FilePath))
                {
                    return JsonConvert.DeserializeObject<List<Target>>(stream.ReadToEnd());
                }
            }
            catch (JsonSerializationException)
            {
                return new List<Target>();
            }
            catch (IOException)
            {
                return new List<Target>();
            }
        }

        public static void Save(IList<Target> targets)
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter stream = new StreamWriter(FilePath))
            using (JsonWriter writer = new JsonTextWriter(stream))
            {
                serializer.Serialize(writer, targets);
            }
        }
    }
}