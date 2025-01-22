public class TableSoort
{
    public string Soort { get; private set; }
    public string Voorkomen { get; private set; }

    public TableSoort(string soort, string voorkomen)
    {
        Soort = soort;
        Voorkomen = voorkomen;
    }
}