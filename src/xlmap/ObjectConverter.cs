using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xlmap
{
    public class ObjectConverter
    {
        private string _baseConfigPath;

        public ObjectConverter()
        {

        }

        public ObjectConverter(string baseConfigPath)
        {
            _baseConfigPath = baseConfigPath;
        }

        public string BaseConfigPath => _baseConfigPath;

        public T Flatten<T, K>(K sourceObj)
        {
            return default;
        }

        public T Expand<T, K>(K sourceObj)
        {
            return default;
        }
    }
}
