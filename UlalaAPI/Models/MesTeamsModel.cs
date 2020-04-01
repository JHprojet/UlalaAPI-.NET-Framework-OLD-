namespace UlalaAPI.Models
{
    public class MesTeamsModel
    {
        public int Id { get; set; }
        public UtilisateurModel Utilisateur { get; set; }
        public TeamModel Team { get; set; }
        public ZoneModel Zone { get; set; }
        public string NomTeam { get; set; }
        public int Actif { get; set; }
    }
}