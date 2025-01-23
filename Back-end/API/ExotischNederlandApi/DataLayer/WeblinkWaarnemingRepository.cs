using Microsoft.Data.Sqlite;

internal class WeblinkWaarnemingRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public WeblinkWaarnemingRepository()
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

    public List<WeblinkWaarneming> HaalAlleWeblinkWaarnemingenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<WeblinkWaarneming>();
        string selectQuery = @"
            SELECT * FROM WEBLINKWAARNEMING;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int wid = reader.GetInt32(0);
            int webid = reader.GetInt32(1);
            soorten.Add(new WeblinkWaarneming
            (
                wid,
                webid
            ));
        }

        return soorten;
    }
    public void VoegWeblinkWaarnemingToe(WeblinkWaarneming weblinkWaarneming)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO WEBLINKWAARNEMING (Wid, Webid)
            VALUES (@Wid, @Webid);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Wid", weblinkWaarneming.Wid);
        command.Parameters.AddWithValue("@Webid", weblinkWaarneming.Webid);


        command.ExecuteNonQuery();
    }

    public void VerwijderWeblinkWaarneming(String wid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM WEBLINKWAARNEMING
        WHERE Wid = @Wid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Wid", wid);

        command.ExecuteNonQuery();
    }
}