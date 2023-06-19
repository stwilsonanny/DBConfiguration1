using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _46_TestAnny
{
    public class DBConfigOptions
    {
        public Func<IDbConnection> CreateDBConnection { get; set; }
        public string TableName { get; set; }
        public bool ReloadOnChange { get; set; }
        public TimeSpan? ReloadInteral { get; set; }
    }
}
