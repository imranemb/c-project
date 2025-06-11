using Bibliotheque.Data;
using Bibliotheque.Models;
using Bibliotheque.Repositories;
using Bibliotheque.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotheque.Controllers;

public static class LivreController
{
    public static void RegisterRoutes(WebApplication app)
    {
        app.MapGet("/livres", async ([FromServices] BibliothequeContext db) =>
        {
            var ebooks = await db.Ebooks.ToListAsync();
            var paperBooks = await db.PaperBooks.ToListAsync();
            var tous = ebooks.Cast<Media>().Concat(paperBooks).ToList();
            return Results.Ok(tous);
        });

        app.MapGet("/livres/search", async ([FromServices] BibliothequeContext db, string? author, string? title, string? sort) =>
        {
            var ebooks = await db.Ebooks.ToListAsync();
            var paperBooks = await db.PaperBooks.ToListAsync();
            var livres = ebooks.Cast<Media>().Concat(paperBooks);

            if (!string.IsNullOrWhiteSpace(author))
                livres = livres.Where(l => l.Auteur.Contains(author, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(title))
                livres = livres.Where(l => l.Titre.Contains(title, StringComparison.OrdinalIgnoreCase));
            if (sort == "author")
                livres = livres.OrderBy(l => l.Auteur);
            else if (sort == "title")
                livres = livres.OrderBy(l => l.Titre);

            return Results.Ok(livres);
        });

        app.MapGet("/livres/{type}/{id:int}", async ([FromServices] BibliothequeContext db, string type, int id) =>
        {
            return type.ToLower() switch
            {
                "ebook" => await db.Ebooks.FindAsync(id) is Ebook e ? Results.Ok(e) : Results.NotFound(),
                "paperbook" => await db.PaperBooks.FindAsync(id) is PaperBook p ? Results.Ok(p) : Results.NotFound(),
                _ => Results.BadRequest("Type inconnu. Utilisez 'ebook' ou 'paperbook'.")
            };
        });

        app.MapPost("/livres/ebook", async ([FromServices] IRepository<Ebook> repo, [FromBody] Ebook ebook) =>
        {
            if (!MiniValidator.TryValidate(ebook, out var errors)) return Results.ValidationProblem(errors);
            await repo.Add(ebook);
            return Results.Created($"/livres/ebook/{ebook.Id}", ebook);
        });

        app.MapPost("/livres/paperbook", async ([FromServices] IRepository<PaperBook> repo, [FromBody] PaperBook paperBook) =>
        {
            if (!MiniValidator.TryValidate(paperBook, out var errors)) return Results.ValidationProblem(errors);
            await repo.Add(paperBook);
            return Results.Created($"/livres/paperbook/{paperBook.Id}", paperBook);
        });

        app.MapPut("/livres/ebook/{id}", async ([FromServices] IRepository<Ebook> repo, int id, [FromBody] Ebook ebook) =>
        {
            if (ebook.Id != id) return Results.BadRequest();
            if (!MiniValidator.TryValidate(ebook, out var errors)) return Results.ValidationProblem(errors);
            await repo.Update(ebook);
            return Results.Ok(ebook);
        });

        app.MapPut("/livres/paperbook/{id}", async ([FromServices] IRepository<PaperBook> repo, int id, [FromBody] PaperBook paperBook) =>
        {
            if (paperBook.Id != id) return Results.BadRequest();
            if (!MiniValidator.TryValidate(paperBook, out var errors)) return Results.ValidationProblem(errors);
            await repo.Update(paperBook);
            return Results.Ok(paperBook);
        });

        app.MapDelete("/livres/ebook/{id}", async ([FromServices] IRepository<Ebook> repo, int id) =>
        {
            await repo.Delete(id);
            return Results.Ok();
        });

        app.MapDelete("/livres/paperbook/{id}", async ([FromServices] IRepository<PaperBook> repo, int id) =>
        {
            await repo.Delete(id);
            return Results.Ok();
        });
    }
}
