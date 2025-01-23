using Microsoft.Data.Sqlite;

internal class GeluidWaarnemingRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public GeluidWaarnemingRepository()
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

    public List<GeluidWaarneming> HaalAlleGeluidWaarnemingenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<GeluidWaarneming>();
        string selectQuery = @"
            SELECT * FROM GELUIDWAARNEMING;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int wid = reader.GetInt32(0);
            int gid = reader.GetInt32(1);
            soorten.Add(new GeluidWaarneming
            (
                wid,
                gid
            ));
        }

        return soorten;
    }
    public void VoegGeluidWaarnemingToe(GeluidWaarneming geluidWaarneming)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO GELUIDWAARNEMING (Wid, Gid)
            VALUES (@Wid, @Gid);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Wid", geluidWaarneming.Wid);
        command.Parameters.AddWithValue("@Gid", geluidWaarneming.Gid);

        command.ExecuteNonQuery();
    }

    public void VerwijderGeluidWaarneming(String wid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM GELUIDWAARNEMING
        WHERE Wid = @Wid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Wid", wid);

        command.ExecuteNonQuery();
    }
}