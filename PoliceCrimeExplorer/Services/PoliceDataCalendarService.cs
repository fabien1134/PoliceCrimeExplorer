namespace PoliceCrimeExplorer.Services
{
    public class PoliceDataCalendarService : IPoliceDataCalendarService
    {
        const int LastMonth = -1;
        const int LastTwoMonths = -2;

        public DateTime GetCalendarMaximumDateBasedOnPoliceDataLastUpdate(DateTime todaysDateOnServer, DateTime lastPoliceDataUpdate)
        {
            var lastWorkingDayOfMonth = GetLastWorkingDayOfMonth(todaysDateOnServer.Year, todaysDateOnServer.Month);
            var previousMonth = lastPoliceDataUpdate.AddMonths(lastPoliceDataUpdate >= lastWorkingDayOfMonth ? LastMonth : LastTwoMonths);
            return GetLastDayOfMonth(previousMonth.Year, previousMonth.Month);
        }

        public DateTime GetLastDayOfMonth(int year, int month) => new(year, month, DateTime.DaysInMonth(year, month));

        public DateTime GetLastWorkingDayOfMonth(int year, int month)
        {
            DateTime lastDayOfMonth = new(year, month, DateTime.DaysInMonth(year, month));

            while (lastDayOfMonth.DayOfWeek == DayOfWeek.Saturday || lastDayOfMonth.DayOfWeek == DayOfWeek.Sunday)
            {
                lastDayOfMonth = lastDayOfMonth.AddDays(-1);
            }

            return lastDayOfMonth;
        }
    }
}
