using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using WebApplication1;
using WebApplication1.Shared;
using WebApplication1.Model;

namespace WebApplication1.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;
        
        
        public void EventClicked()
        {
        }

        private void IncrementCount()
        {
            currentCount++;
            
        }

        
    }
}