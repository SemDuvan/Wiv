public class GebruikerService
{
    private readonly TableGebruikerRepository _repository;

    public GebruikerService()
    {
        _repository = new TableGebruikerRepository();
    }
    public List<TableGebruiker> HaalAlleTableGebruikersOp()
    {
        return _repository.HaalAlleTableGebruikersOp();
    }

    public void RegistreerTableGebruiker(TableGebruiker tableGebruiker)
    {
        _repository.VoegTableGebruikerToe(tableGebruiker);
    }

    public bool VerwijderTableGebruiker(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderTableGebruiker(soort);
        return true;
    }
}