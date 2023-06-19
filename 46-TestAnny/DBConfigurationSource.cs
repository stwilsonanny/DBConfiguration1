using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _46_TestAnny
{
    public class DBConfigurationSource : IConfigurationSource
    {
        public DBConfigOptions options;
        public DBConfigurationSource(DBConfigOptions options) {
            this.options = options;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DBConfigurationProvider(options);
        }
    }
}
