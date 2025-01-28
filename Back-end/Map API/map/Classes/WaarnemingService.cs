using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

public class WaarnemingService
{
    private static readonly HttpClient client = new HttpClient();
    private string apiUrl = "https://api.wiv.one/api/Locaties";
    private const double Tolerance = 0.0001; // Verhoog de tolerantie

    public async Task<int> GetOrAddLocationAsync(double latitude, double longitude)
    {
        // Round the coordinates to 6 decimal places
        latitude = Math.Round(latitude, 6);
        longitude = Math.Round(longitude, 6);

        var locationData = new
        {
            locatienaam = "Onbekend",
            provincie = "Onbekend",
            breedtegraad = latitude.ToString(CultureInfo.InvariantCulture),
            lengtegraad = longitude.ToString(CultureInfo.InvariantCulture)
        };

        Console.WriteLine("Verzonden locatie data:");
        Console.WriteLine(JsonConvert.SerializeObject(locationData, Formatting.Indented));

        var postContent = new StringContent(JsonConvert.SerializeObject(locationData), Encoding.UTF8, "application/json");
        var postResponse = await client.PostAsync(apiUrl, postContent);
        var postResponseString = await postResponse.Content.ReadAsStringAsync();

        Console.WriteLine("Response from adding location:");
        Console.WriteLine(postResponseString);

        // Assuming the location ID is returned in the GET response after adding
        string getUrl = $"{apiUrl}?latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}";
        Console.WriteLine($"GET URL: {getUrl}");
        var getResponse = await client.GetStringAsync(getUrl);
        Console.WriteLine($"GET Response: {getResponse}");
        JArray getJsonArray;
        try
        {
            getJsonArray = JArray.Parse(getResponse);
        }
        catch (JsonReaderException ex)
        {
            throw new Exception($"Error parsing JSON response: {ex.Message}\nResponse: {getResponse}");
        }

        foreach (var location in getJsonArray)
        {
            Console.WriteLine($"Checking location: Latitude = {location["breedtegraad"]}, Longitude = {location["lengtegraad"]}");
            double locLatitude = location["breedtegraad"].Value<double>();
            double locLongitude = location["lengtegraad"].Value<double>();

            if (Math.Abs(locLatitude - latitude) < Tolerance && Math.Abs(locLongitude - longitude) < Tolerance)
            {
                Console.WriteLine($"Match found: Location ID = {location["lid"]}");
                return location["lid"].Value<int>();
            }
        }

        throw new Exception("Location ID not found after adding the location.");
    }
}


