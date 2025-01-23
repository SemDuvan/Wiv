public class LocatieService
{
    private readonly TableLocatieRepository _repository;

    public LocatieService()
    {
        _repository = new TableLocatieRepository();
    }
    public List<TableLocatie> HaalAlleTableLocatiesOp()
    {
        return _repository.HaalAlleTableLocatiesOp();
    }

    public void RegistreerTableLocatie(TableLocatie tableLocatie)
    {
        _repository.VoegTableLocatieToe(tableLocatie);
    }

    public bool VerwijderTableLocatie(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderTableLocatie(soort);
        return true;
    }
}