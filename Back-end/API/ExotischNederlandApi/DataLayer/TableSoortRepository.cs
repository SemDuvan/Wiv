using Microsoft.Data.Sqlite;

internal class TableSoortRepository
{
    private readonly string _connectionString = @"Data Source=C:\Programmeren\Beau\Back-end\API\Scripts\ExotischNederland.db";

    public TableSoortRepository()
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

    public List<TableSoort> HaalAlleSoortenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<TableSoort>();
        string selectQuery = @"
            SELECT * FROM Soort;";
        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            string soort = reader.GetString(0);
            string voorkomen = reader.GetString(1);

            soorten.Add(new TableSoort
            (
                soort,
                voorkomen
            ));
        }

        return soorten;
    }
    public void VoegSoortToe(TableSoort soort)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO Soort (Soort, Voorkomen)
            VALUES (@Soort, @Voorkomen);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Naam", soort.Soort);
        command.Parameters.AddWithValue("@LocatieNaam", soort.Voorkomen);

        command.ExecuteNonQuery();
    }

    public void VerwijderSoort(String soort)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM Soort
        WHERE Soort = @Soort;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Soort", soort);

        command.ExecuteNonQuery();
    }
}