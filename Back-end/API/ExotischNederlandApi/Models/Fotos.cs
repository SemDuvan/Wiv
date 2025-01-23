public class Fotos
{
    public int Fid { get; private set; }
    public string Foto { get; private set; }

    public Fotos(int fid, string foto)
    {
        Fid = fid;
        Foto = foto;
    }
}