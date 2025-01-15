internal class InheemseSoort
{
    public string Naam { get; private set; }
    public string LocatieNaam { get; private set; }
    public Decimal Longitude { get; private set; }

    public Decimal  Latitude {get; private set;}
    public DateTime Datum { get; private set; }

    public InheemseSoort(string naam, string locatieNaam, Decimal longitude, Decimal latitude,DateTime datum)
    {
        Naam = naam;
        LocatieNaam = locatieNaam;
        Datum = datum;
        Longitude = longitude;
        Latitude = latitude;
    }
}