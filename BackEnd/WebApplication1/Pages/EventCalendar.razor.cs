using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Util;
using static WebApplication1.Util.Const;

namespace WebApplication1.Pages
{
    /// <summary>
    /// 달력
    /// </summary>
    public partial class EventCalendar
    {

        /// <summary>
        /// 선택된 년도
        /// </summary>
        private int year = DateTime.Now.Year;
        /// <summary>
        /// 선택된 월
        /// </summary>
        private int month = DateTime.Now.Month;

        /// <summary>
        /// 년도 목록
        /// </summary>
        private IList<int> years = new List<int>();
        /// <summary>
        /// 월 목록
        /// </summary>
        private IList<int> months = new List<int>();

        /// <summary>
        /// 일자 목록
        /// </summary>
        private IList<DayModel> days;

        private IList<DayEvent> dayEvents = new List<DayEvent>();

        

        private bool eventDialogIsOpen = false;

        private bool addEventDialogIsOpen = false;


        private DayEvent dayEvent = new DayEvent();

        private MatBlazor.MatTextField<string> titleTextField;


        private bool viewEventDialogIsOpen = false;
        private DayEvent viewEvent = new DayEvent();


        protected override void OnInitialized()
        {
            InitYear();
            InitMonth();

            InitYears();

            InitMonths();

            CreateMonthEvent();
        }

        /// <summary>
        /// @see https://docs.microsoft.com/ko-kr/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#query-string-and-parse-parameters
        /// </summary>
        private void InitYear()
        {
            string query = new Uri(navigationManager.Uri).Query;
            if (QueryHelpers.ParseQuery(query).TryGetValue("year", out var value))
            {
                year = int.Parse(value);
            }
            else
            {
                year = DateTime.Now.Year;
            }
        }


        /// <summary>
        /// @see https://docs.microsoft.com/ko-kr/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-5.0#query-string-and-parse-parameters
        /// </summary>
        private void InitMonth()
        {
            string query = new Uri(navigationManager.Uri).Query;
            if (QueryHelpers.ParseQuery(query).TryGetValue("month", out var value))
            {
                month = int.Parse(value);
            }
            else
            {
                month = DateTime.Now.Month;
            }
        }



        /// <summary>
        /// 월 목록 초기화
        /// </summary>
        private void InitMonths()
        {
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
        }


        /// <summary>
        /// 년도 목록 초기화
        /// </summary>
        private void InitYears()
        {
            for (int i = year - 5; i < year + 5; i++)
            {
                years.Add(i);
            }
        }


        /// <summary>
        /// 이벤트 클릭
        /// </summary>
        /// <param name="e"></param>
        /// <param name="day"></param>
        /// <param name="id"></param>
        private async void ViewEventClick(MouseEventArgs e, string day, long id)
        {
            Console.WriteLine($"{day} {id}");

            //TODO 대화상자에 표시할 값 설정
            viewEvent = await RequestDayEventAsync(id);


            //대화상자 표시
            viewEventDialogIsOpen = true;
            StateHasChanged();
        }

        private void AddEventClick(EventArgs e, string day)
        {
            dayEvent.De = $"{year}{month.ToString().PadLeft(2,'0')}{day.PadLeft(2,'0')}";

            addEventDialogIsOpen = true;
        }


        private async void AddEvent()
        {
            await SaveDayEventAsync(dayEvent);

            addEventDialogIsOpen = false;
            dayEvent = new DayEvent();

            CreateMonthEvent();
        }


        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="dayEvent"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> SaveDayEventAsync(DayEvent dayEvent)
        {
            return await PmsHttp.PostAsync<DayEvent>(http, API_URI + "/dayevents", sessionStorage, dayEvent);

            //return await http.PostAsJsonAsync<DayEvent>(Const.API_URI + "/dayevents", dayEvent, new JsonSerializerOptions{
            //    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            //});
        }


        /// <summary>
        /// 이전 년도로 이동
        /// </summary>
        private void PrevYearClick()
        {
            year--;

            if (years[0] > year)
            {
                year = years[0];
            }

            CreateMonthEvent();

        }

        /// <summary>
        /// 다음 년도로 이동
        /// </summary>
        private void NextYearClick()
        {
            year++;

            if (years[years.Count - 1] < year)
            {
                year = years[years.Count - 1];
            }

            CreateMonthEvent();
        }



        /// <summary>
        /// 이전 월로 이동
        /// </summary>
        private void PrevMonthClick()
        {
            month--;

            if (1 > month)
            {
                month = 12;
                PrevYearClick();
                return;
            }

            CreateMonthEvent();
        }


        /// <summary>
        /// 다음 월로 이동
        /// </summary>
        private void NextMonthClick()
        {
            month++;

            if (12 < month)
            {
                month = 1;
                NextYearClick();
                return;
            }

            CreateMonthEvent();
        }



        private void YearChange(int y)
        {
            year = y;

            CreateMonthEvent();
        }

        private void MonthChange(int m)
        {
            month = m;

            CreateMonthEvent();
        }




        private async void CreateMonthEvent()
        {
            days = CreateDays();

            //비동기 호출, 리턴, 값 추출하는 방법
            //Task<FiddlerArticle> task = RequestEventsAsync();
            //fiddlerArticle = await task;

            //IDictionary<string, IList<DayEvent>> dic = await RequestEventsAsync();            
            IList<DayEvent> list = await RequestEventsAsync();
            
            BindEvents(list);

            StateHasChanged();
        }

        private void BindEvents(IList<DayEvent> dayEvents)
        {
            foreach (DayModel d in days)
            {
                d.DayEvents = GetDayEventsByYmd(dayEvents, year, month, int.Parse(d.Day));               
            }
        }


        private async Task<DayEvent> RequestDayEventAsync(long id)
        {
            IDictionary<string, DayEvent> dic = await PmsHttp.GetAsync<IDictionary<string, DayEvent>>(http, $"{Const.API_URI}/dayevents/{id}", sessionStorage);
            return dic["data"];
        }

        /// <summary>
        /// @see https://docs.microsoft.com/ko-kr/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0#httpclient-and-httprequestmessage-with-fetch-api-request-options
        /// </summary>
        /// <returns></returns>
        private async Task<IList<DayEvent>> RequestEventsAsync()
        {
            IDictionary<string, IList<DayEvent>> dic = await PmsHttp.GetAsync<IDictionary<string, IList<DayEvent>>>(http, $"{Const.API_URI}/dayevents?year={year}&month={month}", sessionStorage);
            return dic[DATA];
            
        }

        private DayModel GetDay(int weekIndex, int dow)
        {
            foreach(DayModel d in days)
            {
                if(weekIndex == d.WeekIndex && dow == (int)d.Dow)
                {
                    return d;
                }
            }

            return null;
        }

        private IList<DayEvent> GetDayEventsByYmd(IList<DayEvent> list, int year, int month, int day)
        {
            if(null == list)
            {
                return new List<DayEvent>();
            }


            string de = $"{year}{month.ToString().PadLeft(2,'0')}{day.ToString().PadLeft(2,'0')}";

            return list.Where(x => x.De.Equals(de))
                .Select(x => x)
                .ToList();
        }


        private IList<DayModel> CreateDays()
        {
            DateTime dt = new DateTime(year, month, 1);
            int endDay = dt.AddMonths(1).AddDays(-1).Day;
            int weekIndex = 0;

            IList<DayModel> days = new List<DayModel>();

            for(int i=0; i<endDay; i++)
            {
                if(0 != i)
                {
                    dt = dt.AddDays(1);
                }

                days.Add(new DayModel()
                {
                    Dow = dt.DayOfWeek,
                    WeekIndex = weekIndex,
                    Day = dt.Day.ToString(),
                });

                //Console.WriteLine($"{weekIndex} {dt.DayOfWeek} {dt.Day}");

                if(DayOfWeek.Saturday == dt.DayOfWeek)
                {
                    weekIndex++;
                }
            }

            return days;
        }




    }
}