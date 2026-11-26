using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Atividades Formativas",
        Version = "v1",
        Description = "API utilizada na prova prática de Desenvolvimento Web e Mobile I."
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontLocal", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<AtividadesApi.Repositories.IAtividadeRepository,
    AtividadesApi.Repositories.AtividadeRepository>();

var app = builder.Build();

app.UseCors("FrontLocal");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Atividades Formativas v1");
    options.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
