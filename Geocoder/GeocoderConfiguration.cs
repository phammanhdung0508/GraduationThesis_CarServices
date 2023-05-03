using System.Text.Json;
using GraduationThesis_CarServices.Models.DTO.Geocoder;

namespace GraduationThesis_CarServices.Geocoder
{
    public class GeocoderConfiguration
    {
        public async Task<(double Latitude, double Longitude)> GeocodeAsync(string address, string city, string district, string ward)
        {
            string apiKey = "Ap1ujG_wWg4hjLrdaP4YvyprirQXDAEEvYBYEYVXMSrBMA5GOLTToZALN2r8GhO8";
            var location = $"{address}, {ward}, {district}, {city}, Vietnam";
            var encodedAddress = Uri.EscapeDataString(location);
            var requestUrl = $"http://dev.virtualearth.net/REST/v1/Locations?q={Uri.EscapeDataString(location)}&key={apiKey}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var root = JsonSerializer.Deserialize<BingMapsGeocodeResponse>(json);

            if (root?.resourceSets.Length == 0 || root?.resourceSets[0].resources.Length == 0)
            {
                throw new Exception($"No results found for address: {location}");
            }

            var point = root?.resourceSets[0].resources[0].point;
            return (point!.coordinates[0], point.coordinates[1]);
        }
    }
}