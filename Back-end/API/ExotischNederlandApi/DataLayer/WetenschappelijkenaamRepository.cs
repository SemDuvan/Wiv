using Microsoft.Data.Sqlite;

internal class WetenschappelijkenaamRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programming\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public WetenschappelijkenaamRepository()
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

    public List<Wetenschappelijkenaam> HaalAlleWetenschappelijkenamenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<Wetenschappelijkenaam>();
        string selectQuery = @"
            SELECT * FROM WETENSCHAPPELIJKENAAM;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int wnid = reader.GetInt32(0);
            string naam = reader.GetString(1);
            string wetenschappelijkeNaam = reader.GetString(2);
            soorten.Add(new Wetenschappelijkenaam
            (
                wnid,
                naam,
                wetenschappelijkeNaam
            ));
        }

        return soorten;
    }
    public void VoegWetenschappelijkenaamToe(Wetenschappelijkenaam wetenschappelijkeNaam)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO WETENSCHAPPELIJKENAAM (WNid, Naam, WetenschappelijkeNaam)
            VALUES (@WNid, @Naam, @WetenschappelijkeNaam);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@WNid", wetenschappelijkeNaam.WNid);
        command.Parameters.AddWithValue("@Naam", wetenschappelijkeNaam.Naam);
        command.Parameters.AddWithValue("@WetenschappelijkeNaam", wetenschappelijkeNaam.WetenschappelijkeNaam);

        command.ExecuteNonQuery();
    }

    public void VerwijderWetenschappelijkenaam(String naam)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM WETENSCHAPPELIJKENAAM
        WHERE Naam = @Naam;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Naam", naam);

        command.ExecuteNonQuery();
    }
}