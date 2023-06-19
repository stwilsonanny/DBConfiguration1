using _46_TestAnny;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Configuration
{
   public static class DBConfigurationExtensions
    {
        public static IConfigurationBuilder AddDBConfigSource(this IConfigurationBuilder builder,  DBConfigOptions setup) {

            return  builder.Add(new DBConfigurationSource(setup));

        }


        public static IConfigurationBuilder AddDBConfigSource(this IConfigurationBuilder builder,Func<IDbConnection> createDbConnection,string TableName= "T_Configs", bool reloadOnChange=false,TimeSpan? reloadInteral= null)
        {

            return AddDBConfigSource(builder,(new DBConfigOptions { CreateDBConnection = createDbConnection, TableName = TableName, ReloadOnChange = reloadOnChange, ReloadInteral = reloadInteral }));

        }
    }
}
