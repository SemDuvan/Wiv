public class GeluidService
{
    private readonly GeluidRepository _repository;

    public GeluidService()
    {
        _repository = new GeluidRepository();
    }
    public List<Geluid> HaalAlleGeluidsOp()
    {
        return _repository.HaalAlleGeluidsOp();
    }

    public void RegistreerGeluid(Geluid Geluid)
    {
        _repository.VoegGeluidToe(Geluid);
    }

    public bool VerwijderGeluid(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderGeluid(soort);
        return true;
    }
}