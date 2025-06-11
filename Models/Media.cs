using System.ComponentModel.DataAnnotations;

namespace Bibliotheque.Models
{
    public abstract class Media
    {
        public int Id { get; set; }

        [Required]
        public string Titre { get; set; } = string.Empty;

        [Required]
        public string Auteur { get; set; } = string.Empty;

        public abstract string Type { get; }
    }
}
