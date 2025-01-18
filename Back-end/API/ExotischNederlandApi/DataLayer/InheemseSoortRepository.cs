using Microsoft.Data.Sqlite;

internal class InheemseSoortRepository
{
    private readonly string _connectionString = @"Data Source=C:\Users\amkreutzbfh\OneDrive - Zuyd Hogeschool\Modules\Create\Opdrachten\GitHubApi3Tires\Scripts\ExotischNederland.db";

    public InheemseSoortRepository()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        CreateOpenConnection();
    }

    private SqliteConnection CreateOpenConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    public List<InheemseSoort> HaalAlleInheemseSoortenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<InheemseSoort>();
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
    public void VoegInheemseSoortToe(InheemseSoort inheemseSoort)
    {
        var connection = CreateOpenConnection();

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

    public void VerwijderInheemseSoort(String naam)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM InheemseSoort
        WHERE Naam = @Naam;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Naam", naam);

        command.ExecuteNonQuery();
    }
}