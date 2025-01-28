using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class WetenschappelijkeNaamService
{
    private static readonly HttpClient client = new HttpClient();
    private string apiUrl = "https://api.wiv.one/api/WetenschappelijkeNamen";

    public async Task<int> GetOrAddScientificNameAsync(string name, string wetenschappelijkeNaam = "niet beschikbaar")
    {
        // Add the new scientific name
        var nameData = new
        {
            naam = name,
            wetenschappelijkeNaam = wetenschappelijkeNaam
        };

        var postContent = new StringContent(JsonConvert.SerializeObject(nameData), Encoding.UTF8, "application/json");
        var postResponse = await client.PostAsync(apiUrl, postContent);
        var postResponseString = await postResponse.Content.ReadAsStringAsync();

        Console.WriteLine("Response from adding scientific name:");
        Console.WriteLine(postResponseString);

        // Assuming the scientific name ID is returned in the GET response after adding
        string getUrl = $"{apiUrl}?naam={name}";
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

        foreach (var scientificName in getJsonArray)
        {
            if (scientificName["naam"].Value<string>() == name && scientificName["wetenschappelijkeNaam"].Value<string>() == wetenschappelijkeNaam)
            {
                Console.WriteLine($"Match found: Scientific Name ID = {scientificName["wNid"]}");
                return scientificName["wNid"].Value<int>();
            }
        }

        throw new Exception("Scientific Name ID not found after adding the scientific name.");
    }
}





