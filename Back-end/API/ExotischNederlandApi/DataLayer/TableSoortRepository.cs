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

    public List<TableSoort> HaalAlleTableSoortenOp()
    {
        var connection = CreateOpenConnection();

        var soorten = new List<TableSoort>();
        string selectQuery = @"
            SELECT * FROM SOORT;";
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
    public void VoegTableSoortToe(TableSoort tableSoort)
    {
        var connection = CreateOpenConnection();

        string insertQuery = @"
            INSERT INTO SOORT (Soort, Voorkomen)
            VALUES (@Soort, @Voorkomen);";

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@Naam", tableSoort.Soort);
        command.Parameters.AddWithValue("@LocatieNaam", tableSoort.Voorkomen);

        command.ExecuteNonQuery();
    }

    public void VerwijderTableSoort(String soort)
    {
        using var connection = CreateOpenConnection();

        string deleteQuery = @"
        DELETE FROM SOORT
        WHERE Soort = @Soort;";

        using var command = new SqliteCommand(deleteQuery, connection);
        command.Parameters.AddWithValue("@Soort", soort);

        command.ExecuteNonQuery();
    }
}