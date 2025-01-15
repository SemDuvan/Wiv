using Microsoft.Data.Sqlite;

internal class InheemseSoortRepository
{
    private readonly string _connectionString = @"Data Source=C:\Temp\Coding\Zuyd_N_Tier\3-tier-architecture-demo\Scripts\ExotischNederland.db";

    public InheemseSoortRepository()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
    }

    public void VoegInheemseSoortToe(InheemseSoort inheemseSoort)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO InheemseSoort (Naam, LocatieNaam, Longitude, Latitude, Datum)
            VALUES (@Naam, @LocatieNaam, @Longitude, @Latitude, @Datum);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Naam", inheemseSoort.Naam);
        command.Parameters.AddWithValue("@LocatieNaam", inheemseSoort.LocatieNaam);
        command.Parameters.AddWithValue("@Longitude", inheemseSoort.Longitude);
        command.Parameters.AddWithValue("@Latitude", inheemseSoort.Latitude);
        command.Parameters.AddWithValue("@Datum", inheemseSoort.Datum.ToString("yyyy-MM-dd HH:mm:ss"));

        command.ExecuteNonQuery();
    }

    public List<InheemseSoort> HaalAlleInheemseSoortenOp()
    {
        var soorten = new List<InheemseSoort>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"
            SELECT * FROM InheemseSoort;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            string naam = reader.GetString(0);
            string locatieNaam = reader.GetString(1);
            Decimal longitude = reader.GetDecimal(2);
            Decimal latitude = reader.GetDecimal(3);
            string datum = reader.GetString(4);
            soorten.Add(new InheemseSoort
            (
                naam,
                locatieNaam,
                longitude,
                latitude,
                DateTime.ParseExact(datum, "yyyy-MM-dd HH:mm:ss",
                                    System.Globalization.CultureInfo.InvariantCulture)
            ));
        }

        return soorten;
    }
}