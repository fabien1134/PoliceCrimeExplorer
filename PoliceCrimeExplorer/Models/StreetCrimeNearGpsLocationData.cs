namespace PoliceCrimeExplorer.Models
{
    public class StreetCrimeNearGpsLocationData
    {
        public List<CrimeCount> GroupedCrimeCounts { get; set; } = [];
        public List<StreetCrime> StreetCrimes { get; set; } = [];
    }
}
