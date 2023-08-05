using FitnessPredictionMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient in the dependency injection container
builder.Services.AddHttpClient();

builder.Services.AddScoped<IFitnessPredictionService, FitnessPredictionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
