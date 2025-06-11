using Bibliotheque.Clients;
using Bibliotheque.Data;
using Bibliotheque.Repositories;
using Microsoft.EntityFrameworkCore;
using Bibliotheque.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure l'injection de dépendances en utilisant SQLite comme base de données locale
builder.Services.AddDbContext<BibliothequeContext>(opt => opt.UseSqlite("Data Source=bibliotheque.db"));
// Déclare une liaison entre l'interface générique IRepository<T> et son implémentation Repository<T>
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

LivreController.RegisterRoutes(app);

var runTask = app.RunAsync();
await Task.Delay(1000);
await ClientConsole.Run();
await runTask;
