public class TableWeblink
{
    public int Webid { get; private set; }
    public string Weblink { get; private set; }

    public TableWeblink(int webid, string weblink)
    {
        Webid = webid;
        Weblink = weblink;
    }
}