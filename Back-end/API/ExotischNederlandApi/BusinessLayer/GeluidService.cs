public class GeluidService
{
    private readonly TableGeluidRepository _repository;

    public GeluidService()
    {
        _repository = new TableGeluidRepository();
    }
    public List<TableGeluid> HaalAlleTableGeluidsOp()
    {
        return _repository.HaalAlleTableGeluidsOp();
    }

    public void RegistreerTableGeluid(TableGeluid tableGeluid)
    {
        _repository.VoegTableGeluidToe(tableGeluid);
    }

    public bool VerwijderTableGeluid(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderTableGeluid(soort);
        return true;
    }
}