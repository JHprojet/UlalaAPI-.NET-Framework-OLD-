namespace UlalaAPI.Models
{
    public class VoteModel
    {
        public int Id { get; set; }
        public EnregistrementModel Enregistrement { get; set; }
        public UtilisateurModel Utilisateur { get; set; }
        public int Vote { get; set; }
        public int Actif { get; set; }
    }
}