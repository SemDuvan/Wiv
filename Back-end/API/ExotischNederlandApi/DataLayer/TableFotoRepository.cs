using Microsoft.Data.Sqlite;

internal class TableFotoRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public TableFotoRepository()
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

    public List<TableFoto> HaalAlleTableFotosOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<TableFoto>();
        string selectQuery = @"
            SELECT * FROM FOTO;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int fid = reader.GetInt32(0);
            soorten.Add(new TableFoto
            (
                fid
            ));
        }

        return soorten;
    }
    public void VoegTableFotoToe(TableFoto tableFoto)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO SOORT (Fid)
            VALUES (@Fid);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Fid", tableFoto.Fid);

        command.ExecuteNonQuery();
    }

    public void VerwijderTableFoto(String fid)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM FOTO
        WHERE Fid = @Fid;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Fid", fid);

        command.ExecuteNonQuery();
    }
}