using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class DayModel
    {
        public DayOfWeek Dow { get; set; }
        public int WeekIndex { get; set; }
        public string Day { get; set; } = "";
        public IList<DayEvent> DayEvents { get; set; } = null;




        public IList<DayEvent> GetDayEventsByDe(string de)
        {
            if(null == DayEvents)
            {
                return null;
            }

            return DayEvents
                .Where(x => x.De.Equals(de))
                .Select(x => x)
                .ToList();
        }

        public IList<DayEvent> GetDayEventsByYmd(int year, int month, int day)
        {
            return GetDayEventsByDe($"{year}{month.ToString().PadLeft(2,'0')}{day.ToString().PadLeft(2,'0')}");
        }

    }
}
