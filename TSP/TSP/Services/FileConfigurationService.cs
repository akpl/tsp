using System;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TSP.Solver;

namespace TSP.Services
{
    public class FileConfigurationService : IConfigurationService
    {
        private static readonly string FilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "TSP", "configuration.json");

        private Configuration _config;

        public FileConfigurationService()
        {
            _config = ReadFromFile();
        }

        public Configuration Get()
        {
            return _config;
        }

        private static Configuration ReadFromFile()
        {
            Configuration config;

            try
            {
                using (StreamReader stream = new StreamReader(FilePath))
                {
                    config = JsonConvert.DeserializeObject<Configuration>(stream.ReadToEnd());
                }
            }
            catch (JsonSerializationException)
            {
                return new Configuration();
            }
            catch (IOException)
            {
                return new Configuration();
            }

            return config ?? new Configuration();
        }

        public void Save(Configuration config)
        {
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            using (var stream = new StreamWriter(FilePath))
            using (var writer = new JsonTextWriter(stream))
            {
                serializer.Serialize(writer, config);
            }

            _config = config;
        }
    }
}