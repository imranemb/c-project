namespace Bibliotheque.Models
{
    public class Ebook : Media, IReadable
    {
        public int TailleMo { get; set; }

        public override string Type => "Ebook";

        public string DisplayInformation() =>
            $"Ebook - {Titre} de {Auteur}, {TailleMo} Mo";
    }
}
