using Microsoft.Data.Sqlite;

internal class TableWeblinkRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public TableWeblinkRepository()
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

    public List<TableWeblink> HaalAlleTableWeblinksOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<TableWeblink>();
        string selectQuery = @"
            SELECT * FROM WEBLINK;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int webid = reader.GetInt32(0);
            string weblink = reader.GetString(1);
            soorten.Add(new TableWeblink
            (
                webid,
                weblink
            ));
        }

        return soorten;
    }
    public void VoegTableWeblinkToe(TableWeblink tableWeblink)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO SOORT (Webid, Weblink)
            VALUES (@Webid, @Weblink);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Webid", tableWeblink.Webid);
        command.Parameters.AddWithValue("@Weblink", tableWeblink.Weblink);

        command.ExecuteNonQuery();
    }

    public void VerwijderTableWeblink(String webid)
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