using Microsoft.Data.Sqlite;

internal class WeblinkRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public WeblinkRepository()
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

    public List<Weblinks> HaalAlleWeblinksOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Weblinks>();
        string selectQuery = @"
            SELECT * FROM WEBLINK;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int webid = reader.GetInt32(0);
            string weblink = reader.GetString(1);
            soorten.Add(new Weblinks
            (
                webid,
                weblink
            ));
        }

        return soorten;
    }
    public void VoegWeblinkToe(Weblinks Weblink)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO WEBLINK (Webid, Weblink)
            VALUES (@Webid, @Weblink);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Webid", Weblink.Webid);
        command.Parameters.AddWithValue("@Weblink", Weblink.Weblink);

        command.ExecuteNonQuery();
    }

    public void VerwijderWeblink(String webid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM WEBLINK
        WHERE Webid = @Webid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Webid", webid);

        command.ExecuteNonQuery();
    }
}