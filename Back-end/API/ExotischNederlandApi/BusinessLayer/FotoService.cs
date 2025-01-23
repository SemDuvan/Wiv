public class FotoService
{
    private readonly TableFotoRepository _repository;

    public FotoService()
    {
        _repository = new TableFotoRepository();
    }
    public List<TableFoto> HaalAlleTableFotosOp()
    {
        return _repository.HaalAlleTableFotosOp();
    }

    public void RegistreerTableFoto(TableFoto tableFoto)
    {
        _repository.VoegTableFotoToe(tableFoto);
    }

    public bool VerwijderTableFoto(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderTableFoto(soort);
        return true;
    }
}