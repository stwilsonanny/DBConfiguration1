using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace _48_LoggingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection ssc = new ServiceCollection();
            ssc.AddLogging(logBuilder=>{
                logBuilder.AddConsole();
                logBuilder.SetMinimumLevel(LogLevel.Debug);
            });
            ssc.AddScoped<Test1>();

            using (var sp = ssc.BuildServiceProvider()) {

                Test1 t =  sp.GetRequiredService<Test1>();
                t.Test();
            }

        }
    }
}
