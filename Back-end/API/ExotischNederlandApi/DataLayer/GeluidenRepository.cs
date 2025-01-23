using Microsoft.Data.Sqlite;

internal class GeluidenRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public GeluidenRepository()
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

    public List<Geluiden> HaalAlleGeluidenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Geluiden>();
        string selectQuery = @"
            SELECT * FROM GELUID;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int gid = reader.GetInt32(0);
            string geluid = reader.GetString(1);
            soorten.Add(new Geluiden
            (
                    gid,
                    geluid
            ));
        }

        return soorten;
    }
    public void VoegGeluidToe(Geluiden Geluid)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO GELUID (Gid, Geluid)
            VALUES (@Gid, @Geluid);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Gid", Geluid.Gid);
        command.Parameters.AddWithValue("@Geluid", Geluid.Geluid);

        command.ExecuteNonQuery();
    }

    public void VerwijderGeluid(String geluid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM GELUID
        WHERE Geluid = @Geluid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Geluid", geluid);

        command.ExecuteNonQuery();
    }
}