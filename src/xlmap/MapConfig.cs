using System;
using System.Collections.Generic;

namespace xlmap
{
    public class MapConfig
    {
        private string _configBasePath;
        public string ConfigBasePath {
            get
            {
                return _configBasePath;
            } 
            set
            {
                _configBasePath = value;
                foreach (var item in _fieldConfigs)
                    item.ConfigBasePath = _configBasePath;
            }
        }

        /// <summary>
        /// Full assembly name of the class, used by DI to create model instance
        /// </summary>
        public string FullAssemblyName { get; set; }

        /// <summary>
        /// Excel worksheet name. Limited to 31 character. Used by engine to read excel worksheet.
        /// </summary>
        public string WorksheetName { get; set; }

        private List<FieldConfig> _fieldConfigs = new List<FieldConfig>();
        public List<FieldConfig> FieldConfigs => _fieldConfigs;

        /// <summary>
        /// Static method for loading configuration file.
        /// </summary>
        /// <param name="configFile">Configuration file to load</param>
        /// <returns>XlMapConfig object</returns>
        public static MapConfig LoadConfiguration(string configFile, string configBasePath = "")
        {
            if (!string.IsNullOrEmpty(configBasePath))
                configFile = System.IO.Path.Combine(configBasePath, configFile);

            if (string.IsNullOrEmpty(configFile))
                throw new ArgumentNullException(nameof(configFile));

            if (!System.IO.File.Exists(configFile))
                throw new Exception($"File {configFile} not found");

            string jsonString = System.IO.File.ReadAllText(configFile);
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<MapConfig>(jsonString);
            config.ConfigBasePath = configBasePath;
            return config;
        }
    }
}
