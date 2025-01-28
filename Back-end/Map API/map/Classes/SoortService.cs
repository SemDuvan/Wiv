using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class SoortService
{
    private static readonly HttpClient client = new HttpClient();
    private string apiUrl = "https://api.wiv.one/api/Soorten";

    public async Task<int> GetOrAddSpeciesAsync(string soort, string voorkomen)
    {
        // Check if the species already exists
        string getUrl = $"{apiUrl}?soort={soort}&voorkomen={voorkomen}";
        var getResponse = await client.GetStringAsync(getUrl);
        JArray getJsonArray;
        try
        {
            getJsonArray = JArray.Parse(getResponse);
        }
        catch (JsonReaderException ex)
        {
            throw new Exception($"Error parsing JSON response: {ex.Message}\nResponse: {getResponse}");
        }

        foreach (var species in getJsonArray)
        {
            if (species["soort"].Value<string>() == soort && species["voorkomen"].Value<string>() == voorkomen)
            {
                return species["sid"].Value<int>();
            }
        }

        // Add the new species
        var speciesData = new
        {
            soort = soort,
            voorkomen = voorkomen
        };

        var postContent = new StringContent(JsonConvert.SerializeObject(speciesData), Encoding.UTF8, "application/json");
        var postResponse = await client.PostAsync(apiUrl, postContent);
        var postResponseString = await postResponse.Content.ReadAsStringAsync();

        Console.WriteLine("Response from adding species:");
        Console.WriteLine(postResponseString);

        // Assuming the species ID is returned in the GET response after adding
        getResponse = await client.GetStringAsync(getUrl);
        Console.WriteLine($"GET Response: {getResponse}");
        try
        {
            getJsonArray = JArray.Parse(getResponse);
        }
        catch (JsonReaderException ex)
        {
            throw new Exception($"Error parsing JSON response: {ex.Message}\nResponse: {getResponse}");
        }

        foreach (var species in getJsonArray)
        {
            if (species["soort"].Value<string>() == soort && species["voorkomen"].Value<string>() == voorkomen)
            {
                Console.WriteLine($"Match found: Species ID = {species["sid"]}");
                return species["sid"].Value<int>();
            }
        }

        throw new Exception("Species ID not found after adding the species.");
    }
}

