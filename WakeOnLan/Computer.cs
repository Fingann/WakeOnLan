using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeOnLan
{

    public class Rootobject
    {
        public List<Computer> Computers { get; set; }
    }

    public class Computer
    {
        public string MAC { get; set; }
        public string Name { get; set; }
    }
}