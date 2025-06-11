using Microsoft.EntityFrameworkCore;
using Bibliotheque.API.Data; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliothequeContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Bibliotheque API", Version = "v1" });
});


builder.Services.AddScoped<IRepository<Media>, MediaRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bibliotheque API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BibliothequeContext>();
        context.Database.EnsureCreated(); 
        Console.WriteLine("Base de données vérifiée et prête");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'initialisation de la base: {ex.Message}");
    }
}

app.Run();