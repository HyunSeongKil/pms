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
using MatBlazor;
using System.Text.Json;
using WebApplication1.Util;
using static WebApplication1.Util.Const;

namespace WebApplication1.Pages
{
    public partial class Signin
    {
        /// <summary>
        /// �α��� ����
        /// </summary>
        private User user = new();

        /// <summary>
        /// ������Ʈ ���� dialog ǥ�� ����
        /// </summary>
        private bool projectSelectDialogIsOpen = false;


        /// <summary>
        /// �α��� ����ڰ� ������ �� �ִ� ������Ʈ ���
        /// </summary>
        private IList<UserProjectMapng> userProjectMapngs = new List<UserProjectMapng>();

        /// <summary>
        /// dialog���� ������ ������Ʈ ���̵�
        /// </summary>
        private int selectedProjectId = -1;


        /// <summary>
        /// �α��� Ŭ��
        /// </summary>
        private async void SigninClick()
        {
            bool b = await SigninAsync();
            if (b)
            {
                //����
                //navigationManager.NavigateTo("eventCalendar");
                GetProjectsAndShow();
            }
            else
            {
                //����
                toaster.Add("�α��� ����", MatToastType.Warning);
                //await Task.Delay(2000);
                //navigationManager.NavigateTo("eventCalendar");
            }
        }

        /// <summary>
        /// ������Ʈ ��� ��ȸ & ǥ��
        /// </summary>
        private async void GetProjectsAndShow()
        {
            IDictionary<string,IList<UserProjectMapng>> dic = await PmsHttp.GetAsync<IDictionary<string, IList<UserProjectMapng>>>(http, $"{API_URI}/user_project_mapngs?userId={user.UserId}", sessionStorage);
            this.userProjectMapngs = dic[DATA];
            
            this.projectSelectDialogIsOpen = true;
            
            StateHasChanged();
        }


        /// <summary>
        /// �α���
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SigninAsync()
        {
            HttpResponseMessage message = await http.PostAsJsonAsync<User>(BASE_URI + "/signin", user, new JsonSerializerOptions()
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            });


            IDictionary<string, object> dic = await message.Content.ReadFromJsonAsync<IDictionary<string, object>>();


            //�α��� �����̸�
            if (0 == dic["resultMessage"].ToString().Length)
            {
                //��ū ����
                await sessionStorage.SetItemAsync(TOKEN, dic[TOKEN]);
                return true;
            }


            return false;
        }


        /// <summary>
        /// ��� Ŭ��
        /// </summary>
        private void CancelClick()
        {
            user = new();
        }


        /// <summary>
        /// ������ ������Ʈ ���̵� ����
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        private async Task<bool> PutProjectIdAsync(int projectId)
        {
            Project p = new()
            {
                ProjectId = projectId
            };

            //
            HttpResponseMessage response = await PmsHttp.PutAsync<Project>(http, $"{BASE_URI}/signin/projects", sessionStorage, p);

            IDictionary<string, object> dic = await response.Content.ReadFromJsonAsync<IDictionary<string, object>>();
            //Console.WriteLine(dic.Keys.Count);

            return (0 == dic.Keys.Count);
        }



        /// <summary>
        /// ������Ʈ ���� Ŭ��
        /// </summary>
        private async void ProjectSelectClick()
        {
            //
            bool b = await PutProjectIdAsync(selectedProjectId);

            if (b)
            {
                //����
                navigationManager.NavigateTo("eventCalendar");
            }
            else
            {
                toaster.Add("������ �߻��߽��ϴ�. �α��� �������� �̵��մϴ�.", MatToastType.Warning);
                await Task.Delay(1000);
                ProjectSelectCancelClick();
            }

        }


        /// <summary>
        /// ������Ʈ ���� ��� Ŭ��
        /// </summary>
        private void ProjectSelectCancelClick()
        {
            navigationManager.NavigateTo("signin");
        }
    }
}