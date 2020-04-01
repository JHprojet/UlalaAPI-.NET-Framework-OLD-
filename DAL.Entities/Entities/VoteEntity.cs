namespace DAL.Entities
{
    public class VoteEntity
    {
        public int Id { get; set; }
        public int EnregistrementId { get; set; }
        public int UtilisateurId { get; set; }
        public int Vote { get; set; }
        public int Actif { get; set; }
    }
}