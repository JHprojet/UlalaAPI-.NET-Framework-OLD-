namespace UlalaAPI.Models
{
    public class FavoriModel
    {
        public int Id { get; set; }
        public UtilisateurModel Utilisateur { get; set; }
        public EnregistrementModel Enregistrement { get; set; }
        public int Actif { get; set; }
    }
}