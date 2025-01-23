public class TableSoortService
{
    private readonly TableSoortRepository _repository;

    public TableSoortService()
    {
        _repository = new TableSoortRepository();
    }
    public List<TableSoort> HaalAlleTableSoortenOp()
    {
        return _repository.HaalAlleTableSoortenOp();
    }

    public void RegistreerTableSoort(TableSoort tableSoort)
    {
        _repository.VoegTableSoortToe(tableSoort);
    }

    public bool VerwijderTableSoort(String soort)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderTableSoort(soort);
        return true;
    }
}