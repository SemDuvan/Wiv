using Microsoft.Data.Sqlite;

internal class TableLocatieRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public TableLocatieRepository()
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

    public List<TableLocatie> HaalAlleTableLocatiesOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<TableLocatie>();
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
            soorten.Add(new TableLocatie
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
    public void VoegTableLocatieToe(TableLocatie tableLocatie)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO LOCATIE (Lid, Locatienaam, Provincie, Breedtegraad, Lengtegraad)
            VALUES (@Lid, @Locatienaam, @Provincie, @Breedtegraad, @Lengtegraad);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Lid", tableLocatie.Lid);
        command.Parameters.AddWithValue("@Locatienaam", tableLocatie.Locatienaam);
        command.Parameters.AddWithValue("@Provincie", tableLocatie.Provincie);
        command.Parameters.AddWithValue("@Breedtegraat", tableLocatie.Breedtegraad);
        command.Parameters.AddWithValue("@Lengtegraad", tableLocatie.Lengtegraad);

        command.ExecuteNonQuery();
    }

    public void VerwijderTableLocatie(String lid)
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