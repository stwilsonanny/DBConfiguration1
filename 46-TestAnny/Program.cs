using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace _46_TestAnny
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection sc = new ServiceCollection();
            sc.AddScoped<TestController>();

            ConfigurationBuilder cb = new ConfigurationBuilder();
            cb.AddJsonFile("appsetting.json");
            var croot =  cb.Build();
            var conStr =  croot.GetSection("ConnectionString").Value;
            cb.AddDBConfigSource(new DBConfigOptions { CreateDBConnection = () => new SqlConnection(conStr),TableName="T_Config", ReloadOnChange=true,ReloadInteral=new TimeSpan(4) }  );
            croot =  cb.Build();

            Console.WriteLine(croot.GetSection("FTP").Value.ToString());
            sc.AddOptions().Configure<FTP>(t=> croot.GetSection("FTP").Bind(t));

            using (var sp = sc.BuildServiceProvider()) {
               var controller=   sp.GetRequiredService<TestController>();
                controller.Test();
            }

        }
    }
}
