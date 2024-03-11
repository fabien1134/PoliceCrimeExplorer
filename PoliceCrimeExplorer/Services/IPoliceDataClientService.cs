using PoliceCrimeExplorer.Models;

namespace PoliceCrimeExplorer.Services
{
    public interface IPoliceDataClientService
    {
        Task<DateTime> GetLastPoliceDataUpdate();
        Task<StreetCrimeNearGpsLocationData> GetStreetCrimeNearGpsLocation(string latitudeGpsCord, string longitudeGpsCord, DateTime searchDate);
    }
}
