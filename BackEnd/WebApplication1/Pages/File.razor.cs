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
using  static  WebApplication1 . Util . Const ;
using MatBlazor;
using System.Text.Json;
using WebApplication1.Util;
using WebApplication1.Model;

namespace WebApplication1.Pages
{
    public partial class File
    {
        private IList<string> dirs = new List<string>()
        {
            "."
        };
        
        private IList<ServerFile> files = new List<ServerFile>();

        private int directoryCo = 0;
        private int fileCo = 0;
        private long fileSize = 0;


        protected override async Task OnInitializedAsync()
        {
            RequestFilesAndShow();
        }

        private async void ServerFileClick(EventArgs e, bool isDirectory, string name)
        {
            if (isDirectory)
            {
                dirs.Add(name);

                RequestFilesAndShow();
            }
        }


        private async void BreadcumbClick(EventArgs e, int index)
        {
            for(int i=dirs.Count-1; i>=0; i--)
            {
                if(index < i)
                {
                    dirs.RemoveAt(i);
                }
            }

            RequestFilesAndShow();
        }

        private async void UpDirClick()
        {
            dirs.RemoveAt(dirs.Count - 1);

            RequestFilesAndShow();
        }

        private async void Click()
        {
            dirs.Clear();
            dirs.Add(".");

            RequestFilesAndShow();
            
        }



        private async void RequestFilesAndShow()
        {
            this.files = await RequestFiles();

            directoryCo = 0;
            fileCo = 0;
            fileSize = 0;
            foreach(ServerFile f in this.files)
            {
                if (f.IsDirectory)
                {
                    directoryCo++;
                }
                else
                {
                    fileCo++;
                    fileSize += f.Size;
                }
            }

            StateHasChanged();
        }

        private async Task<IList<ServerFile>> RequestFiles()
        {
            string dir = string.Join('/', dirs);

            IDictionary<string, object> dic = await PmsHttp.GetAsync<IDictionary<string, object>>(http, $"{API_URI}/files?dir={dir}", sessionStorage);
            object obj = dic[Const.DATA];
            
            JsonElement je = (JsonElement)obj;
            var json = je.GetRawText();
            
            return JsonSerializer.Deserialize<IList<ServerFile>>(json);
        }
    }
}