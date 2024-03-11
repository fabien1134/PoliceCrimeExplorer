using PoliceCrimeExplorer.Models;
using System.Text.Json;

namespace PoliceCrimeExplorer.Services
{
    public class PoliceDataClientService(HttpClient httpClient) : IPoliceDataClientService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<DateTime> GetLastPoliceDataUpdate()
        {
            var response = await _httpClient.GetAsync("crime-last-updated");

            response.EnsureSuccessStatusCode();

            return ParseDateFromJson(await response.Content.ReadAsStringAsync());
        }

        public async Task<StreetCrimeNearGpsLocationData> GetStreetCrimeNearGpsLocation(string latitudeGpsCord, string longitudeGpsCord, DateTime searchDate)
        {
            var streetCrimeNearGpsLocationData = new StreetCrimeNearGpsLocationData();

            var response = await _httpClient.GetAsync($"crimes-street/all-crime?lat={latitudeGpsCord}&lng={longitudeGpsCord}&date={searchDate:yyyy-MM}");
            response.EnsureSuccessStatusCode();

            string jsonData = await response.Content.ReadAsStringAsync();

            streetCrimeNearGpsLocationData.StreetCrimes = JsonSerializer.Deserialize<List<StreetCrime>>(jsonData) ?? [];

            streetCrimeNearGpsLocationData.GroupedCrimeCounts = GetGroupedCrimeCounts(streetCrimeNearGpsLocationData.StreetCrimes);

            return streetCrimeNearGpsLocationData;
        }

        private List<CrimeCount> GetGroupedCrimeCounts(List<StreetCrime> streetCrimes)
        {
            var groupedCrimeCount = new Dictionary<string, CrimeCount>();

            foreach (var streetCrime in streetCrimes)
            {
                if (groupedCrimeCount.TryGetValue(streetCrime.category, out var crimeCount))
                {
                    crimeCount.Count++;
                }
                else
                {
                    groupedCrimeCount.Add(streetCrime.category, new CrimeCount { CrimeName = streetCrime.category, Count = 1 });
                }
            }

            return [.. groupedCrimeCount.Values];
        }

        private static DateTime ParseDateFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));

            JsonDocument doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("date", out var dateProperty)) throw new ArgumentException("Unable to get the date from the value.", nameof(json));

            if (!DateTime.TryParse(dateProperty.ToString(), out DateTime result)) throw new ArgumentException("Failed to parse date.", nameof(json));

            return result;
        }
    }
}
