public class WeblinkService
{
    private readonly TableWeblinkRepository _repository;

    public WeblinkService()
    {
        _repository = new TableWeblinkRepository();
    }
    public List<TableWeblink> HaalAlleTableWeblinksOp()
    {
        return _repository.HaalAlleTableWeblinksOp();
    }

    public void RegistreerTableWeblink(TableWeblink tableWeblink)
    {
        _repository.VoegTableWeblinkToe(tableWeblink);
    }

    public bool VerwijderTableWeblink(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderTableWeblink(soort);
        return true;
    }
}