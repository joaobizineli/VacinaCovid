using VacinaCovid.Negocios.Interface.INegocios;
using VacinaCovid.Negocios.Negocios;
using VacinaCovid.Repositorio.Interface.IRepositorios;
using VacinaCovid.Repositorio.Repositorio;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPacienteNegocio, PacienteNegocio>();
builder.Services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();

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