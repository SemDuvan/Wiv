public class TableSoortService
{
    private readonly InheemseSoortRepository _repository;

    public TableSoortService()
    {
        _repository = new TableSoortRepository();
    }
    public List<TableSoort> HaalAlleSoortenOp()
    {
        return _repository.HaalAlleSoortenOp();
    }

    public void RegistreerSoort(TableSoort soort)
    {
        _repository.VoegInheemseSoortToe(soort);
    }

    public bool VerwijderInheemseSoort(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderSoort(soort);
        return true;
    }
}