using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class ServerFile
    {
        public string Name { get; set; } = "";
        public long Size { get; set; } = 0;
        public bool IsDirectory { get; set; } = false;
        public string Date { get; set; } = "";
    }
}
