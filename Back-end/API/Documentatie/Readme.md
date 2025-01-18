# Handleiding: Het Drie-Lagen Model in .Net en C# voor Beginners

## Inleiding

In deze handleiding leer je hoe je een applicatie bouwt volgens het drie-lagenmodel waarbij alle lagen binnen één project blijven. Dit is ideaal voor beginnende developers die nog geen gebruik willen maken van gedeelde libraries. We bouwen een applicatie waarin inheemse soorten kunnen worden geregistreerd en weergegeven.

---

## Wat is het Drie-Lagen Model?

Het drie-lagenmodel bestaat uit drie logische lagen die gescheiden worden binnen je applicatie:

1. **Presentatielaag (UI):** De gebruikersinterface waarmee de gebruiker interacteert.
2. **Businesslaag (BL):** De logica en regels van de applicatie.
3. **Data-laag (DAL):** Het beheer van data-opslag en gegevensbeheer.

Deze structuur helpt om de code overzichtelijk en onderhoudbaar te houden.

---

## Projectstructuur

Hier is een voorbeeldstructuur van een project met het drie-lagenmodel:

```plaintext
Project
|-- DataLayer
|   |-- InheemseSoortRepository.cs
|
|-- BusinessLayer
|   |-- InheemseSoortService.cs
|
|-- PresentationLayer
    |-- Program.cs
```

### Overzicht van Functionaliteiten

- **DataLayer:** Opslag en ophalen van gegevens.
- **BusinessLayer:** Logica voor het toevoegen en ophalen van inheemse soorten.
- **PresentationLayer:** Interactie met de gebruiker via een consolemenu.

---

## Stappenplan

### Stap 1: DataLayer Implementatie

In de **DataLayer** beheren we de gegevensopslag. Voor dit voorbeeld slaan we data op in een eenvoudige lijst.

**Bestand:** `DataLayer/InheemseSoortRepository.cs`

```csharp
using System.Collections.Generic;

namespace DataLayer
{
    public class InheemseSoortRepository
    {
        private readonly List<InheemseSoort> _soorten = new();

        public void VoegInheemseSoortToe(InheemseSoort soort)
        {
            _soorten.Add(soort);
        }

        public List<InheemseSoort> HaalAlleInheemseSoortenOp()
        {
            return _soorten;
        }
    }

    public class InheemseSoort
    {
        public string Naam { get; set; }
        public string Locatie { get; set; }
        public string Datum { get; set; }
    }
}
```

**Checkvraag:**

- Hoe kun je de klasse `InheemseSoort` uitbreiden met extra eigenschappen zonder bestaande functionaliteit te breken?
- Hoe zorgt de klasse `InheemseSoortRepository` ervoor dat de data veilig beheerd wordt?

### Stap 2: BusinessLayer Implementatie

De **BusinessLayer** bevat de logica voor het beheren van inheemse soorten.

**Bestand:** `BusinessLayer/InheemseSoortService.cs`

```csharp
using System.Collections.Generic;
using DataLayer;

namespace BusinessLayer
{
    public class InheemseSoortService
    {
        private readonly InheemseSoortRepository _repository;

        public InheemseSoortService()
        {
            _repository = new InheemseSoortRepository();
        }

        public void RegistreerInheemseSoort(string naam, string locatie, string datum)
        {
            var soort = new InheemseSoort
            {
                Naam = naam,
                Locatie = locatie,
                Datum = datum
            };

            _repository.VoegInheemseSoortToe(soort);
        }

        public List<InheemseSoort> HaalAlleInheemseSoortenOp()
        {
            return _repository.HaalAlleInheemseSoortenOp();
        }
    }
}
```

**Checkvraag:**

- Hoe kun je de klasse `InheemseSoortService` herbruikbaar maken zonder dat deze afhankelijk is van een specifieke implementatie van `InheemseSoortRepository`?
- Waarom is het belangrijk om validaties in de `BusinessLayer` te plaatsen in plaats van in de `DataLayer`?

### Stap 3: PresentationLayer Implementatie

De **PresentationLayer** verzorgt de interactie met de gebruiker via de console.

**Bestand:** `PresentationLayer/Program.cs`

```csharp
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/inheemseSoorten", () =>
{
    InheemseSoortService inheemseSoortService = new InheemseSoortService();    
    return inheemseSoortService.HaalAlleInheemseSoortenOp();
})
.WithName("GetInheemseSoorten");

app.MapPost("/add", ([FromBody]InheemseSoort inheemseSoort) =>
{
    InheemseSoortService inheemseSoortService = new InheemseSoortService();
    inheemseSoortService.RegistreerInheemseSoort(inheemseSoort.Naam, inheemseSoort.LocatieNaam, inheemseSoort.Longitude, inheemseSoort.Latitude, inheemseSoort.Datum);
})
.WithName("Add");

app.Run();
```

**Checkvraag:**

- Hoe kun je de gebruikersinterface uitbreiden met minimale impact op de andere lagen?
- Waarom is het belangrijk dat de `PresentationLayer` geen directe toegang heeft tot de `DataLayer`?

---

## Testen

- Start de applicatie in Visual Studio.
- Gebruik het menu om inheemse soorten te registreren en weer te geven.
- Controleer of de data correct wordt opgeslagen en weergegeven.

**Checkvraag:**

- Hoe kun je OOP-principes zoals encapsulatie en polymorfisme toepassen om deze applicatie beter schaalbaar te maken?

---

## Reflectie

- Wat heb je geleerd over het drie-lagenmodel?
- Hoe helpt het scheiden van verantwoordelijkheden om je code overzichtelijk te houden?
- Welke OOP-concepten kun je toepassen om de applicatie verder te verbeteren?

---

Met deze handleiding heb je een eenvoudige, gestructureerde applicatie gebouwd volgens het drie-lagenmodel. Veel succes!