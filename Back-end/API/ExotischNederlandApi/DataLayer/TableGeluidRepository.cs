using Microsoft.Data.Sqlite;

internal class TableGeluidRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public TableGeluidRepository()
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

    public List<TableGeluid> HaalAlleTableGeluidsOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<TableGeluid>();
        string selectQuery = @"
            SELECT * FROM GELUID;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int gid = reader.GetInt32(0);
            soorten.Add(new TableGeluid
            (
                    gid
            ));
        }

        return soorten;
    }
    public void VoegTableGeluidToe(TableGeluid tableGeluid)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO SOORT (Gid)
            VALUES (@Gid);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Gid", tableGeluid.Gid);

        command.ExecuteNonQuery();
    }

    public void VerwijderTableGeluid(String gid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM GELUID
        WHERE Gid = @Gid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Gid", gid);

        command.ExecuteNonQuery();
    }
}