namespace DAL.Entities
{
    public class EnregistrementEntity
    { 
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public int BossZoneId { get; set; }
        public int TeamId { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public int Note { get; set; }
        public int Actif { get; set; }
    }
}