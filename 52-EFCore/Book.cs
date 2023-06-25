using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _52_EFCore
{
    public class Book
    {
        public long Id { set; get; }
        public string Title { set; get; }
        public DateTime PubTime { set; get; }
        public Double Price { set; get; }

    }
}
