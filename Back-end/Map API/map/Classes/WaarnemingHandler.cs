using System;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

// Add the missing PushData class
public class PushData
{
    public int Wid { get; set; }
    public string Omschrijving { get; set; }
    public string Toelichting { get; set; }
    public DateTime Datum { get; set; }
    public TimeSpan Tijd { get; set; }
    public int Sid { get; set; }
    public int WNid { get; set; }
    public int Lid { get; set; }
    public int Aantal { get; set; }
    public string Geslacht { get; set; }
    public string Gebruiker { get; set; }
    public string Zekerheid { get; set; }
    public int Webid { get; set; }
    public string ManierDelen { get; set; }
    public string Soort { get; set; }
    public bool Zeldzaam { get; set; }
}

public class WaarnemingHandler
{
    private readonly WaarnemingService waarnemingService;
    private readonly SoortService soortService;
    private readonly WetenschappelijkeNaamService wetenschappelijkeNaamService;

    public WaarnemingHandler(WaarnemingService waarnemingService, SoortService soortService, WetenschappelijkeNaamService wetenschappelijkeNaamService)
    {
        this.waarnemingService = waarnemingService;
        this.soortService = soortService;
        this.wetenschappelijkeNaamService = wetenschappelijkeNaamService;
    }

    public async Task VerzendWaarneming(string name, string soort, bool zeldzaam, string description, DateTime datum, TimeSpan tijd, double latitude, double longitude, int aantal)
    {
        try
        {
            int locationId = await waarnemingService.GetOrAddLocationAsync(latitude, longitude);
            if (locationId == 0)
            {
                MessageBox.Show("Kon locatie niet ophalen of toevoegen.", "Fout");
                return;
            }

            string voorkomen = zeldzaam ? "Zeldzaam" : "Algemeen";
            int speciesId = await soortService.GetOrAddSpeciesAsync(soort, voorkomen);
            if (speciesId == 0)
            {
                MessageBox.Show("Kon soort niet ophalen of toevoegen.", "Fout");
                return;
            }

            int scientificNameId = await wetenschappelijkeNaamService.GetOrAddScientificNameAsync(name);
            if (scientificNameId == 0)
            {
                MessageBox.Show("Kon wetenschappelijke naam niet ophalen of toevoegen.", "Fout");
                return;
            }

            int nextWid = await GetNextWidAsync();

            var data = new PushData
            {
                Wid = nextWid,
                Omschrijving = name,
                Toelichting = description,
                Datum = datum,
                Tijd = tijd,
                Sid = speciesId,
                WNid = scientificNameId,
                Lid = locationId,
                Aantal = aantal,
                Geslacht = "NVT",
                Gebruiker = "jan@example.com",
                Zekerheid = "NVT",
                Webid = 1,
                ManierDelen = "Applicatie",
                Soort = soort,
                Zeldzaam = zeldzaam
            };

            Console.WriteLine("Verzonden data:");
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            await PostDataToApiAsync(data);

            MessageBox.Show($"{soort} {name} {description}\nDatum en tijd: {datum:g}", "Invoer");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout");
            Console.WriteLine($"Er is een fout opgetreden: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    private async Task<int> GetNextWidAsync()
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://api.wiv.one/api/Waarnemingen";
            var response = await client.GetStringAsync(apiUrl);
            JArray jsonArray = JArray.Parse(response);

            if (jsonArray.Count == 0)
            {
                return 1;
            }

            int maxWid = jsonArray.Select(item => item["wid"].Value<int>()).Max();
            return maxWid + 1;
        }
    }

    private async Task PostDataToApiAsync(PushData data)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://api.wiv.one/api/Waarnemingen";
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Error: {response.StatusCode} - {errorMessage}");
                throw new Exception($"API Error: {response.StatusCode} - {errorMessage}");
            }
        }
    }
}
