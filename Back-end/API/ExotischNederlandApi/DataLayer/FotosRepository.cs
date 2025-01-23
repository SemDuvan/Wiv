using Microsoft.Data.Sqlite;

internal class FotosRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public FotosRepository()
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

    public List<Fotos> HaalAlleFotosOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Fotos>();
        string selectQuery = @"
            SELECT * FROM FOTO;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int fid = reader.GetInt32(0);
            string foto = reader.GetString(1);
            soorten.Add(new Fotos
            (
                fid,
                foto
            ));
        }

        return soorten;
    }
    public void VoegFotoToe(Fotos Foto)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO FOTO (Fid, Foto)
            VALUES (@Fid, @Foto);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Fid", Foto.Fid);
        command.Parameters.AddWithValue("@Foto", Foto.Foto);

        command.ExecuteNonQuery();
    }

    public void VerwijderFoto(String foto)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM FOTO
        WHERE Foto = @Foto;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Foto", foto);

        command.ExecuteNonQuery();
    }
}