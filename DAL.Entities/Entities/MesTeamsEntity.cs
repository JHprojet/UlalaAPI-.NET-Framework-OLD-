namespace DAL.Entities
{
    public class MesTeamsEntity
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public int TeamId { get; set; }
        public int ZoneId { get; set; }
        public string NomTeam { get; set; }
        public int Actif { get; set; }
    }
}