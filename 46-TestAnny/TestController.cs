using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _46_TestAnny
{
    public class TestController
    {
        private readonly IOptionsSnapshot<FTP> options;
        
        public  TestController(IOptionsSnapshot<FTP> options){
            this.options=options;
        }

        public void Test() {
            Console.WriteLine("1");
            Console.WriteLine(options.Value.ContentType);
        
        }
    }
}
