using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class UserProjectMapng
    {
        public int UserProjectMapngId { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }

    }
}
