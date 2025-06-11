using System.Net.Http.Json;
using System.Text.Json;

namespace Bibliotheque.Clients;

public static class ClientConsole
{
    public static async Task Run()
    {
        var client = new HttpClient { BaseAddress = new Uri("http://localhost:5185") };
        Console.WriteLine("Bienvenue dans le client console de la bibliothèque numérique");

        while (true)
        {
            Console.WriteLine("\n1. Liste des livres\n2. Rechercher\n3. Ajouter\n4. Modifier\n5. Supprimer\n6. Quitter");
            var choix = Console.ReadLine();
            if (choix == "6") break;

            try
            {
                switch (choix)
                {
                    case "1":
                        var res = await client.GetFromJsonAsync<List<JsonElement>>("/livres");
                        res?.ForEach(l => Console.WriteLine(l.ToString()));
                        break;
                    case "2":
                        Console.Write("Auteur : "); var a = Console.ReadLine();
                        Console.Write("Titre : "); var t = Console.ReadLine();
                        var l = await client.GetFromJsonAsync<List<JsonElement>>($"/livres/search?author={a}&title={t}");
                        l?.ForEach(x => Console.WriteLine(x.ToString()));
                        break;
                    case "3":
                        Console.Write("Type (ebook/paperbook): "); var ty = Console.ReadLine();
                        Console.Write("Titre : "); var ti = Console.ReadLine();
                        Console.Write("Auteur : "); var au = Console.ReadLine();
                        if (ty == "ebook")
                        {
                            Console.Write("Taille Mo : "); if (!int.TryParse(Console.ReadLine(), out var mo)) break;
                            await client.PostAsJsonAsync("/livres/ebook", new { Titre = ti, Auteur = au, TailleMo = mo });
                        }
                        else
                        {
                            Console.Write("Pages : "); if (!int.TryParse(Console.ReadLine(), out var p)) break;
                            await client.PostAsJsonAsync("/livres/paperbook", new { Titre = ti, Auteur = au, NombrePages = p });
                        }
                        break;
                    case "4":
                        Console.Write("Type : "); var typ = Console.ReadLine();
                        Console.Write("Id : "); if (!int.TryParse(Console.ReadLine(), out var id)) break;
                        Console.Write("Titre : "); var nt = Console.ReadLine();
                        Console.Write("Auteur : "); var na = Console.ReadLine();
                        if (typ == "ebook")
                        {
                            Console.Write("Taille Mo : "); if (!int.TryParse(Console.ReadLine(), out var tm)) break;
                            await client.PutAsJsonAsync($"/livres/ebook/{id}", new { Id = id, Titre = nt, Auteur = na, TailleMo = tm });
                        }
                        else
                        {
                            Console.Write("Pages : "); if (!int.TryParse(Console.ReadLine(), out var pg)) break;
                            await client.PutAsJsonAsync($"/livres/paperbook/{id}", new { Id = id, Titre = nt, Auteur = na, NombrePages = pg });
                        }
                        break;
                    case "5":
                        Console.Write("Type : "); var dt = Console.ReadLine();
                        Console.Write("Id : "); var did = Console.ReadLine();
                        await client.DeleteAsync($"/livres/{dt}/{did}");
                        break;
                    default:
                        Console.WriteLine("Choix invalide");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
            }
        }
    }
}
