namespace DAL.Entities
{
    public class StrategyEntity
    { 
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BossZoneId { get; set; }
        public int CharactersConfigurationId { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public int Note { get; set; }
        public int Active { get; set; }
    }
}