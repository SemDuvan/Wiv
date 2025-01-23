public class TableSoort
{
    public int Sid { get; private set; }
    public string Soort { get; private set; }
    public string Voorkomen { get; private set; }

    public TableSoort(int sid, string soort, string voorkomen)
    {
        Sid = sid;
        Soort = soort;
        Voorkomen = voorkomen;
    }
}