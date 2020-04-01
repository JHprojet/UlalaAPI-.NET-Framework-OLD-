namespace DAL.Entities
{
    public class FavoriEntity
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public int EnregistrementId { get; set; }
        public int Actif { get; set; }
    }
}