namespace PoliceCrimeExplorer.ViewModels
{
    public class PoliceDataUpdateViewModel
    {
        public DateTime LastPoliceDataUpdate { get; set; }
        public DateTime CalendarMaxDate { get; set; }
        public bool SuccessfullyRetrievedLastPoliceDataUpdate { get; set; }
    }
}
