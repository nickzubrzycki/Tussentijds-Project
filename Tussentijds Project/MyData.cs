using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tussentijds_Project
{
    public class MyData
    {
        public Order myVar { get; set; }
    }

    public static class AllData
    {
        public static MyData myData { get; set; }
    }
}
