namespace PoliceCrimeExplorer.Services
{
    public interface IPoliceDataCalendarService
    {
        /// <summary>
        /// Based on the date the server was last updated, we calculate the maximum date the user can select with their crime search, may be one or two months prior
        /// </summary>
        /// <param name="todaysDateOnServer"></param>
        /// <param name="lastPoliceDataUpdate"></param>
        /// <returns></returns>
        DateTime GetCalendarMaximumDateBasedOnPoliceDataLastUpdate(DateTime todaysDateOnServer, DateTime lastPoliceDataUpdate);
        DateTime GetLastDayOfMonth(int year, int month);
        DateTime GetLastWorkingDayOfMonth(int year, int month);
    }
}
