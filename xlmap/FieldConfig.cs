using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xlmap
{
    public class FieldConfig
    {
        /// <summary>
        /// Data type enumeration, used to determine type of data from a property
        /// </summary>
        public enum DataType
        {
            INTEGER,
            DOUBLE,
            STRING,
            DATETIME,
            OBJECT
        }

        public string ConfigBasePath { get; set; }
        public bool IsForeignKey { get; set; } = false;

        /// <summary>
        /// Index position of a column to read. Start from 1 (Column A in excel)
        /// </summary>
        public int? ColumnIndex { get; set; } = null;

        /// <summary>
        /// Column title. Used to read from (if column index null), or write to excel file as column header.
        /// </summary>
        public string ColumnTitle { get; set; }

        /// <summary>
        /// Property name to write to, or read from.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Property data type information.
        /// </summary>
        public DataType PropertyDataType { get; set; }

        /// <summary>
        /// Reference configuration file, if property data type is object.
        /// </summary>
        public string RefConfigFile { get; set; }

        private MapConfig _xlMapConfig = null;
        /// <summary>
        /// Reference configuration object, loaded from RefConfigFile property, if property data type is object.
        /// </summary>
        public MapConfig RefConfiguration { get => _xlMapConfig; }

        /// <summary>
        /// Method for loading reference configuration file. Method will accept config file name parameter,
        /// or use RefConfigFile content property if parameter not exist.
        /// </summary>
        /// <param name="configFile">Configuration file to load</param>
        /// <returns>XlMapConfig object</returns>
        public MapConfig LoadReferenceConfigFile(string configFile = "")
        {
            if (string.IsNullOrEmpty(configFile))
                configFile = this.RefConfigFile;
            else
                this.RefConfigFile = configFile;

            if (string.IsNullOrEmpty(configFile))
                throw new ArgumentNullException(nameof(configFile));

            this._xlMapConfig = MapConfig.LoadConfiguration(configFile, ConfigBasePath);
            return _xlMapConfig;
        }
    }
}
