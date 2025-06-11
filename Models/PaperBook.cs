namespace Bibliotheque.Models
{
    public class PaperBook : Media, IReadable
    {
        public int NombrePages { get; set; }

        public override string Type => "Livre Papier";

        public string DisplayInformation() =>
            $"Livre Papier - {Titre} de {Auteur}, {NombrePages} pages";
    }
}
