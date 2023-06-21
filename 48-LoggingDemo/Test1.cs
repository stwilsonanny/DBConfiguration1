using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _48_LoggingDemo
{
    class Test1
    {
        private readonly ILogger<Test1> logger;
        public Test1(ILogger<Test1> logger) {
            this.logger = logger;
        }

        public void Test() {
            logger.LogDebug("开始执行数据库同步");
            logger.LogDebug("链接数据库成功");
            logger.LogWarning("查找数据失败，重试第一次");
            logger.LogWarning("查找数据失败，重试第二次");
            logger.LogError("查找数据失败");

            try {
                File.ReadAllText("A:/1.txt");
                logger.LogDebug("读写成功");
                        
                
            } catch(Exception ex){
                logger.LogError(ex, "读取文件失败");
            
            }
            
        }
    }
}
