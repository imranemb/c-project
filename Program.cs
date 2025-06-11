using Microsoft.EntityFrameworkCore;
using Bibliotheque.API.Data;
using Bibliotheque.API.Models; // Ajout pour les modèles
using Bibliotheque.API.Repositories; // Ajout pour les repositories

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données
builder.Services.AddDbContext<BibliothequeContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});

// Configuration des contrôleurs avec validation
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Garde le casse d'origine
    });

// Configuration Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "API Bibliothèque Numérique", 
        Version = "v1",
        Description = "API pour la gestion de livres électroniques et papier",
        Contact = new() { Name = "Votre Nom", Email = "votre@email.com" }
    });
    
    // Active les commentaires XML pour la documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Configuration des services
builder.Services.AddScoped<IRepository<Media>, MediaRepository>();
builder.Services.AddAutoMapper(typeof(Program)); // Si vous utilisez AutoMapper

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Bibliothèque v1");
        c.RoutePrefix = "api-docs"; // Définit une route personnalisée
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // Ajouté avant Authorization
app.UseAuthorization();

app.MapControllers();

// Initialisation de la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BibliothequeContext>();
        
        if (context.Database.IsSqlite())
        {
            context.Database.Migrate(); // Préférable à EnsureCreated pour les migrations
        }
        
        await SeedData.Initialize(context); // Pour peupler la base si nécessaire
        Console.WriteLine("Base de données initialisée avec succès");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erreur lors de l'initialisation de la base de données");
    }
}

app.Run();
