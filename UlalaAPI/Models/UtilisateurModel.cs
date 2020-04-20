using System.ComponentModel.DataAnnotations;

namespace UlalaAPI.Models
{
    public class UtilisateurModel
    {
        public int Id { get; set; }
        public string Pseudo { get; set; }
        public string Mail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        public int Actif { get; set; }
        public string ActivationToken { get; set; }
    }
}