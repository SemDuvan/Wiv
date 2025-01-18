public class InheemseSoortService
{
    private readonly InheemseSoortRepository _repository;

    public InheemseSoortService()
    {
        _repository = new InheemseSoortRepository();
    }
    public List<InheemseSoort> HaalAlleInheemseSoortenOp()
    {
        return _repository.HaalAlleInheemseSoortenOp();
    }

    public void RegistreerInheemseSoort(InheemseSoort inheemseSoort)
    {
        _repository.VoegInheemseSoortToe(inheemseSoort);
    }

    public bool VerwijderInheemseSoort(String naam)
    {
        //TODO: implementeer 
        //var soort = _repository.HaalInheemseSoortOp(naam);
        //if (soort == null)
        //{
        //    return false;
        //}

        _repository.VerwijderInheemseSoort(naam);
        return true;
    }
}