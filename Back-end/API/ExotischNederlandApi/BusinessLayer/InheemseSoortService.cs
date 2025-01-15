internal class InheemseSoortService
{
    private readonly InheemseSoortRepository _repository;

    public InheemseSoortService()
    {
        _repository = new InheemseSoortRepository();
    }

    public void RegistreerInheemseSoort(string naam, string locatieNaam, Decimal longitude, Decimal latitude, DateTime datum)
    {
        var soort = new InheemseSoort
        (
            naam,
            locatieNaam,
            longitude,
            latitude,
            datum
        );

        _repository.VoegInheemseSoortToe(soort);
    }

    public List<InheemseSoort> HaalAlleInheemseSoortenOp()
    {
        return _repository.HaalAlleInheemseSoortenOp();
    }
}