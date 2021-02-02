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
        /// 로그인 정보
        /// </summary>
        private User user = new();

        /// <summary>
        /// 프로젝트 선택 dialog 표시 여부
        /// </summary>
        private bool projectSelectDialogIsOpen = false;


        /// <summary>
        /// 로그인 사용자가 접근할 수 있는 프로젝트 목록
        /// </summary>
        private IList<UserProjectMapng> userProjectMapngs = new List<UserProjectMapng>();

        /// <summary>
        /// dialog에서 선택한 프로젝트 아이디
        /// </summary>
        private int selectedProjectId = -1;


        /// <summary>
        /// 로그인 클릭
        /// </summary>
        private async void SigninClick()
        {
            bool b = await SigninAsync();
            if (b)
            {
                //성공
                //navigationManager.NavigateTo("eventCalendar");
                GetProjectsAndShow();
            }
            else
            {
                //실패
                toaster.Add("로그인 실패", MatToastType.Warning);
                //await Task.Delay(2000);
                //navigationManager.NavigateTo("eventCalendar");
            }
        }

        /// <summary>
        /// 프로젝트 목록 조회 & 표시
        /// </summary>
        private async void GetProjectsAndShow()
        {
            IDictionary<string,IList<UserProjectMapng>> dic = await PmsHttp.GetAsync<IDictionary<string, IList<UserProjectMapng>>>(http, $"{API_URI}/user_project_mapngs?userId={user.UserId}", sessionStorage);
            this.userProjectMapngs = dic[DATA];
            
            this.projectSelectDialogIsOpen = true;
            
            StateHasChanged();
        }


        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SigninAsync()
        {
            HttpResponseMessage message = await http.PostAsJsonAsync<User>(BASE_URI + "/signin", user, new JsonSerializerOptions()
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
            });


            IDictionary<string, object> dic = await message.Content.ReadFromJsonAsync<IDictionary<string, object>>();


            //로그인 성공이면
            if (0 == dic["resultMessage"].ToString().Length)
            {
                //토큰 저장
                await sessionStorage.SetItemAsync(TOKEN, dic[TOKEN]);
                return true;
            }


            return false;
        }


        /// <summary>
        /// 취소 클릭
        /// </summary>
        private void CancelClick()
        {
            user = new();
        }


        /// <summary>
        /// 선택한 프로젝트 아이디 설정
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
        /// 프로젝트 선택 클릭
        /// </summary>
        private async void ProjectSelectClick()
        {
            //
            bool b = await PutProjectIdAsync(selectedProjectId);

            if (b)
            {
                //성공
                navigationManager.NavigateTo("eventCalendar");
            }
            else
            {
                toaster.Add("오류가 발생했습니다. 로그인 페이지로 이동합니다.", MatToastType.Warning);
                await Task.Delay(1000);
                ProjectSelectCancelClick();
            }

        }


        /// <summary>
        /// 프로젝트 선택 취소 클릭
        /// </summary>
        private void ProjectSelectCancelClick()
        {
            navigationManager.NavigateTo("signin");
        }
    }
}