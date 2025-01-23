using Microsoft.Data.Sqlite;

internal class FotoRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public FotoRepository()
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

    public List<Foto> HaalAlleFotosOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Foto>();
        string selectQuery = @"
            SELECT * FROM FOTO;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int fid = reader.GetInt32(0);
            soorten.Add(new Foto
            (
                fid
            ));
        }

        return soorten;
    }
    public void VoegFotoToe(Foto Foto)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO SOORT (Fid)
            VALUES (@Fid);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Fid", Foto.Fid);

        command.ExecuteNonQuery();
    }

    public void VerwijderFoto(String fid)
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