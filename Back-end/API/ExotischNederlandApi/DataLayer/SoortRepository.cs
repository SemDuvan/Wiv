using Microsoft.Data.Sqlite;

internal class SoortRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public SoortRepository()
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

    public List<Soorten> HaalAlleSoortenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Soorten>();
        string selectQuery = @"
            SELECT * FROM SOORT;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int sid = reader.GetInt32(0);
            string soort = reader.GetString(1);
            string voorkomen = reader.GetString(2);
            soorten.Add(new Soorten
            (
                sid,
                soort,
                voorkomen
            ));
        }

        return soorten;
    }
    public void VoegSoortToe(Soorten Soort)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO SOORT (Sid, Soort, Voorkomen)
            VALUES (@Sid, @Soort, @Voorkomen);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Sid", Soort.Sid);
        command.Parameters.AddWithValue("@Naam", Soort.Soort);
        command.Parameters.AddWithValue("@LocatieNaam", Soort.Voorkomen);

        command.ExecuteNonQuery();
    }

    public void VerwijderSoort(String soort)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM SOORT
        WHERE Soort = @Soort;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Soort", soort);

        command.ExecuteNonQuery();
    }
}