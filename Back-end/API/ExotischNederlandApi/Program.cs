var builder = WebApplication.CreateBuilder(args);

// Voeg services toe aan de container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Voeg Swagger/OpenAPI toe

// Voeg jouw service toe aan de DI-container
builder.Services.AddScoped<InheemseSoortService>();

// Voeg CORS-beleid toe
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configureer de HTTP-request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Activeer Swagger
    app.UseSwaggerUI(); // Activeer Swagger UI
}

// Gebruik CORS-beleid
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Zorg ervoor dat controllers worden gemapped

app.Run();
