using System.ComponentModel.DataAnnotations;

namespace Bibliotheque.Utils
{
    public static class MiniValidator
    {
        public static bool TryValidate<T>(T model, out Dictionary<string, string[]> errors)
        {
            var context = new ValidationContext(model!);
            var results = new List<ValidationResult>(); // Liste qui contiendra les erreurs de validation éventuelles
            var isValid = Validator.TryValidateObject(model!, context, results, true); // Lance la validation de tous les attributs 

            // Transforme la liste de ValidationResult en un dictionnaire : 
            errors = results
                .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage!).ToArray());

            return isValid;
        }
    }
}
