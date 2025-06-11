using System;
using System.Threading.Tasks;
using ClientBibliotheque.Services; 

namespace ClientBibliotheque
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Client Bibliothèque ===");

            bool quitter = false;
            while (!quitter)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Afficher la liste des livres");
                Console.WriteLine("2. Rechercher un livre");
                Console.WriteLine("3. Ajouter un livre");
                Console.WriteLine("4. Modifier un livre");
                Console.WriteLine("5. Supprimer un livre");
                Console.WriteLine("0. Quitter");

                Console.Write("Choix: ");
                var choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        await LivreService.AfficherLivres();
                        break;
                    case "2":
                        await LivreService.RechercherLivre();
                        break;
                    case "3":
                        await LivreService.AjouterLivre();
                        break;
                    case "4":
                        await LivreService.ModifierLivre();
                        break;
                    case "5":
                        await LivreService.SupprimerLivre();
                        break;
                    case "0":
                        quitter = true;
                        break;
                    default:
                        Console.WriteLine(" Choix invalide.");
                        break;
                }
            }

            Console.WriteLine("\n Merci d'avoir utilisé le gestionnaire de bibliothèque !");
        }
    }
}