using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TSP.Solver;

namespace TSP.Services
{
    public class TargetsFileStorage : ITargetsService
    {
        private static readonly string FilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TSP", "tsp.json");

        public List<Target> Read()
        {
            List<Target> list;

            try
            {
                using (StreamReader stream = new StreamReader(FilePath))
                {
                    list = JsonConvert.DeserializeObject<List<Target>>(stream.ReadToEnd());
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

            return list ?? new List<Target>();
        }

        public void Save(IList<Target> targets)
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