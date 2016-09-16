using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WakeOnLan
{
    class ComputerNameServer
    {

        public Rootobject GetComputers()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory+  "ComputerList.json";
            return JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(path));
        }

    }
}
