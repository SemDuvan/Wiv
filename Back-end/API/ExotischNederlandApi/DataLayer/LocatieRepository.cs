using Microsoft.Data.Sqlite;

internal class LocatieRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public LocatieRepository()
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

    public List<Locatie> HaalAlleLocatiesOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Locatie>();
        string selectQuery = @"
            SELECT * FROM LOCATIE;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int lid = reader.GetInt32(0);
            string locatienaam = reader.GetString(1);
            string provincie = reader.GetString(2);
            float breedtegraad = reader.GetFloat(3);
            float lengtegraad = reader.GetFloat(4);
            soorten.Add(new Locatie
            (
                lid,
                locatienaam,
                provincie,
                breedtegraad,
                lengtegraad
            ));
        }

        return soorten;
    }
    public void VoegLocatieToe(Locatie Locatie)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO LOCATIE (Lid, Locatienaam, Provincie, Breedtegraad, Lengtegraad)
            VALUES (@Lid, @Locatienaam, @Provincie, @Breedtegraad, @Lengtegraad);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Lid", Locatie.Lid);
        command.Parameters.AddWithValue("@Locatienaam", Locatie.Locatienaam);
        command.Parameters.AddWithValue("@Provincie", Locatie.Provincie);
        command.Parameters.AddWithValue("@Breedtegraat", Locatie.Breedtegraad);
        command.Parameters.AddWithValue("@Lengtegraad", Locatie.Lengtegraad);

        command.ExecuteNonQuery();
    }

    public void VerwijderLocatie(String lid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM LOCATIE
        WHERE Lid = @Lid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Lid", lid);

        command.ExecuteNonQuery();
    }
}