public class FotoService
{
    private readonly FotoRepository _repository;

    public FotoService()
    {
        _repository = new FotoRepository();
    }
    public List<Foto> HaalAlleFotosOp()
    {
        return _repository.HaalAlleFotosOp();
    }

    public void RegistreerFoto(Foto Foto)
    {
        _repository.VoegFotoToe(Foto);
    }

    public bool VerwijderFoto(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderFoto(soort);
        return true;
    }
}