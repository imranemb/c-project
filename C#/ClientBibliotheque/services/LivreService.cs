using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientBibliotheque;
using Newtonsoft.Json;

namespace ClientBibliotheque.Services
{
  public static class LivreService

  {
    private static readonly string apiUrl = "http://localhost:5000/livres";

    public static async Task AfficherLivres()
    {
      try
      {
        Console.WriteLine("\nChargement des livres...");

        using (HttpClient client = new HttpClient())
        {

          HttpResponseMessage response = await client.GetAsync(apiUrl);
          response.EnsureSuccessStatusCode();

          string jsonResponse = await response.Content.ReadAsStringAsync();
          List<Livre> livres = JsonConvert.DeserializeObject<List<Livre>>(jsonResponse);

          Console.WriteLine($"\n {livres.Count} livre(s) trouvé(s) :\n");

          foreach (var livre in livres)
          {
            Console.WriteLine($"- ID: {livre.Id}");
            Console.WriteLine($"  Titre: {livre.Title}");
            Console.WriteLine($"  Auteur: {livre.Author}");
            Console.WriteLine($"  Type: {livre.Type}");
            Console.WriteLine();
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($" Erreur : {ex.Message}");
      }
    }

    public static async Task AjouterLivre()
    {
      try
      {
        Console.WriteLine("\n=== Ajouter un nouveau livre ===");

        Console.Write("Titre : ");
        string titre = Console.ReadLine();

        Console.Write("Auteur : ");
        string auteur = Console.ReadLine();

        Console.Write("Type (Ebook ou PaperBook) : ");
        string type = Console.ReadLine();

        var nouveauLivre = new Livre
        {
          Title = titre,
          Author = auteur,
          Type = type
        };

        using (HttpClient client = new HttpClient())
        {
          // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

          string json = JsonConvert.SerializeObject(nouveauLivre);
          var content = new StringContent(json, Encoding.UTF8, "application/json");

          HttpResponseMessage response = await client.PostAsync(apiUrl, content);

          if (response.IsSuccessStatusCode)
          {
            Console.WriteLine(" Livre ajouté avec succès !");
          }
          else
          {
            string erreur = await response.Content.ReadAsStringAsync();
            Console.WriteLine($" Erreur {response.StatusCode} : {erreur}");
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($" Erreur : {ex.Message}");
      }
    }

    public static async Task RechercherLivre()
    {
      try
      {
        Console.WriteLine("\n=== Rechercher un livre ===");

        Console.Write("Titre (laisser vide si non) : ");
        string titre = Console.ReadLine();

        Console.Write("Auteur (laisser vide si non) : ");
        string auteur = Console.ReadLine();

        // Construction dynamique de l’URL avec paramètres
        var url = new StringBuilder(apiUrl + "?");

        if (!string.IsNullOrEmpty(titre))
          url.Append($"Title={Uri.EscapeDataString(titre)}&");

        if (!string.IsNullOrEmpty(auteur))
          url.Append($"Author={Uri.EscapeDataString(auteur)}&");

        // Nettoyage final du & ou ?
        string finalUrl = url.ToString().TrimEnd('&', '?');

        using (HttpClient client = new HttpClient())
        {
          // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

          HttpResponseMessage response = await client.GetAsync(finalUrl);
          response.EnsureSuccessStatusCode();

          string json = await response.Content.ReadAsStringAsync();
          List<Livre> livres = JsonConvert.DeserializeObject<List<Livre>>(json);

          Console.WriteLine($"\n🔍 {livres.Count} résultat(s) trouvé(s) :\n");

          foreach (var livre in livres)
          {
            Console.WriteLine($"- ID: {livre.Id}");
            Console.WriteLine($"  Titre: {livre.Title}");
            Console.WriteLine($"  Auteur: {livre.Author}");
            Console.WriteLine($"  Type: {livre.Type}");
            Console.WriteLine();
          }
        }
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine($" Erreur réseau : {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($" Erreur : {ex.Message}");
      }
    }


    public static async Task ModifierLivre()
    {
      try
      {
        Console.WriteLine("\n=== Modifier un livre ===");

        Console.Write("ID du livre à modifier : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
          Console.WriteLine(" ID invalide.");
          return;
        }

        Console.Write("Nouveau titre : ");
        string titre = Console.ReadLine();

        Console.Write("Nouvel auteur : ");
        string auteur = Console.ReadLine();

        Console.Write("Nouveau type (Ebook ou PaperBook) : ");
        string type = Console.ReadLine();

        var livreModifie = new Livre
        {
          Title = titre,
          Author = auteur,
          Type = type
        };

        using (HttpClient client = new HttpClient())
        {
          string url = $"{apiUrl}/{id}";

          // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

          string json = JsonConvert.SerializeObject(livreModifie);
          var content = new StringContent(json, Encoding.UTF8, "application/json");

          HttpResponseMessage response = await client.PutAsync(url, content);

          if (response.IsSuccessStatusCode)
          {
            Console.WriteLine(" Livre modifié avec succès !");
          }
          else
          {
            string erreur = await response.Content.ReadAsStringAsync();
            Console.WriteLine($" Erreur {response.StatusCode} : {erreur}");
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($" Erreur : {ex.Message}");
      }
    }


    public static async Task SupprimerLivre()
{
    try
    {
        Console.WriteLine("\n=== Supprimer un livre ===");

        Console.Write("ID du livre à supprimer : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine(" ID invalide.");
            return;
        }

        using (HttpClient client = new HttpClient())
        {
            string url = $"{apiUrl}/{id}";

            HttpResponseMessage response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(" Livre supprimé avec succès !");
            }
            else
            {
                string erreur = await response.Content.ReadAsStringAsync();
                Console.WriteLine($" Erreur {response.StatusCode} : {erreur}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($" Erreur : {ex.Message}");
    }
}



  }

}