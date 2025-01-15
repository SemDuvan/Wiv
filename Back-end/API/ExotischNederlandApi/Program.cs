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

